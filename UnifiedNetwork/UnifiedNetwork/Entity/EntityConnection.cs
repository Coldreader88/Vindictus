using System;
using System.Collections.Generic;
using System.Text;
using Devcat.Core;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity.Operations;
using UnifiedNetwork.EntityGraph;
using UnifiedNetwork.PipedNetwork;
using Utility;

namespace UnifiedNetwork.Entity
{
	public class EntityConnection : IEntityProxy, IEntityAdapter
	{
		public IEntity LocalEntity
		{
			get
			{
				return this.MyEntity;
			}
		}

		public object Tag { get; set; }

		public bool OwnerConnection { get; internal set; }

		public Entity MyEntity
		{
			get
			{
				return this.myEntity;
			}
			private set
			{
				if (this.myEntity != null)
				{
					this.myEntity.UnbindConnection(this);
				}
				this.myEntity = value;
				if (this.myEntity != null)
				{
					this.myEntity.BindConnection(this);
				}
			}
		}

		public long RemoteID { get; private set; }

		public string RemoteCategory { get; private set; }

		public bool IsIncoming { get; set; }

		public bool IsOutgoing
		{
			get
			{
				return !this.IsIncoming;
			}
		}

		public bool IsClosed { get; private set; }

		public Pipe Pipe { get; set; }

		public OperationProxy Proxy { get; set; }

		private Func<Type, Func<Operation, OperationProcessor>> ProcessorBuilder { get; set; }

		private Action<long, string, Action<IEntity, BindEntityResult>, bool> EntityMaker { get; set; }

		private bool IsClosing { get; set; }

		private bool IsForcedClosing { get; set; }

		public event EventHandler<EventArgs<IEntityProxy>> OperationQueueOversized;

		public EntityConnection.ConnectionState State { get; set; }

		public EntityConnection(Entity local, Func<Type, Func<Operation, OperationProcessor>> processorBuilder, Action<long, string, Action<IEntity, BindEntityResult>, bool> entityMaker)
		{
			if (processorBuilder == null)
			{
				throw new ArgumentNullException("processorBuilder");
			}
			if (entityMaker == null)
			{
				throw new ArgumentNullException("entityMaker");
			}
			this.ProcessorBuilder = processorBuilder;
			this.EntityMaker = entityMaker;
			this.MyEntity = local;
			this.State = EntityConnection.ConnectionState.Initial;
		}

		~EntityConnection()
		{
			if (!this.IsClosed)
			{
				FileLog.Log("ThreadUnsafe.log", string.Format("{0}[{1}] {2}", this, this.LocalEntity, (this.LocalEntity != null) ? this.LocalEntity.Tag : "[NULL]"));
				Log<EntityConnection>.Logger.ErrorFormat(CallerInfo.Get("z:\\066B\\global2\\HeroesCode\\server\\UnifiedNetwork\\Entity\\EntityConnection.cs", 165), new object[]
				{
					"Connection [{{ LocalEntity.Category = {0}, PeerCategory = {1} }} ({2})] leaks",
					(this.LocalEntity != null) ? this.LocalEntity.Category : "(null)",
					this.RemoteCategory,
					this.IsIncoming ? "incoming" : "outgoing"
				});
			}
		}

		public void RegisterMessageHandler(Type messageType, Action<EntityConnection, object> handler)
		{
			if (this.MessageHandlers.ContainsKey(messageType))
			{
				Dictionary<Type, Action<EntityConnection, object>> messageHandlers;
				(messageHandlers = this.MessageHandlers)[messageType] = (Action<EntityConnection, object>)Delegate.Combine(messageHandlers[messageType], handler);
				return;
			}
			this.MessageHandlers[messageType] = handler;
		}

		public void UnregisterMessageHandler(Type messageType, Action<EntityConnection, object> handler)
		{
			if (this.MessageHandlers.ContainsKey(messageType))
			{
				Dictionary<Type, Action<EntityConnection, object>> messageHandlers;
				(messageHandlers = this.MessageHandlers)[messageType] = (Action<EntityConnection, object>)Delegate.Remove(messageHandlers[messageType], handler);
			}
		}

		public void RequestOperation(Operation op)
		{
			if (this.State == EntityConnection.ConnectionState.Initial || this.State == EntityConnection.ConnectionState.Bound || this.State == EntityConnection.ConnectionState.Closed || this.State == EntityConnection.ConnectionState.OutOfEntity)
			{
				Log<EntityConnection>.Logger.InfoFormat("Cannot request {0} to connection state {1}", op, this.State);
				op.IssueFailEvent();
				return;
			}
			if (this.IsClosed)
			{
				Log<EntityConnection>.Logger.InfoFormat("Cannot request {0} to closed connection : {1} {2}", op, this.IsClosing, this.IsClosed);
				op.IssueFailEvent();
				return;
			}
			if (this.IsClosing)
			{
				Log<EntityConnection>.Logger.InfoFormat("request {0} to closing connection : {1} {2} {3}", new object[]
				{
					op,
					this.IsClosing,
					this.IsClosed,
					this.IsForcedClosing
				});
				if (this.IsForcedClosing)
				{
					op.IssueFailEvent();
					return;
				}
			}
			if (this.State != EntityConnection.ConnectionState.Connected || (this.Proxy != null && this.Proxy.IsProcessing))
			{
				if (this.OperationQueue.Count > 100 && this.OperationQueueOversized != null)
				{
					this.OperationQueueOversized(this.LocalEntity, new EventArgs<IEntityProxy>(this));
				}
				this.OperationQueue.Enqueue(op);
				if (this.State != EntityConnection.ConnectionState.Connected)
				{
					this.ConnectionFailed += delegate(object sender, EventArgs<IEntityProxy> e)
					{
						op.IssueFailEvent();
					};
					return;
				}
			}
			else
			{
				if (this.Proxy == null)
				{
					this.Proxy = new OperationProxy(delegate(object msg)
					{
						this.SendMessage(msg);
					}, null);
					this.Proxy.ProcessorAttached += this.Proxy_ProcessorAttached;
					this.Proxy.ProcessorDisposed += this.Proxy_ProcessorDisposed;
				}
				this.Proxy.RequestOperation(op);
			}
		}

		private void Proxy_ProcessorAttached(OperationProxy proxy, OperationProcessor processor)
		{
			if (processor is EntityProcessor)
			{
				(processor as EntityProcessor).Connection = this;
			}
		}

		private void Proxy_ProcessorAttached<TOperation, TEntity>(OperationProxy proxy, EntityProcessor<TOperation, TEntity> processor) where TOperation : Operation where TEntity : class
		{
			processor.Connection = this;
		}

		private void Proxy_ProcessorDisposed(OperationProxy proxy)
		{
			if (OperationProxy.IsTimeReportEnabled)
			{
				string text = proxy.RecentOperationName;
				if (text != "")
				{
					if (proxy.IsRequesting)
					{
						text += "\tout";
					}
					else
					{
						text += "\tin";
					}
					OperationTimeReportElement operationTimeReportElement;
					if (!this.OperationTimeReport.TryGetValue(text, out operationTimeReportElement))
					{
						operationTimeReportElement = new OperationTimeReportElement();
					}
					operationTimeReportElement.Add(proxy.TimerResult);
					this.OperationTimeReport[text] = operationTimeReportElement;
				}
			}
			if (this.Proxy != null)
			{
				this.Proxy.Dispose();
				this.Proxy = null;
			}
			if (this.State == EntityConnection.ConnectionState.OutOfEntity)
			{
				this.DoClose();
				return;
			}
			if (this.OperationQueue.Count > 0)
			{
				this.RequestOperation(this.OperationQueue.Dequeue());
				return;
			}
			if (this.IsClosing)
			{
				if (this.State == EntityConnection.ConnectionState.Bound)
				{
					this.SendMessage(new RequestClose());
					return;
				}
				this.DoClose();
			}
		}

		public void Close()
		{
			this.Close(false);
		}

		public void Close(bool isForced)
		{
			if (this.IsForcedClosing && this.IsClosing)
			{
				return;
			}
			if (!isForced && this.IsClosing)
			{
				return;
			}
			this.IsClosing = true;
			if (isForced)
			{
				this.IsForcedClosing = true;
			}
			if (isForced || this.Proxy == null)
			{
				if (this.State == EntityConnection.ConnectionState.Connected || this.State == EntityConnection.ConnectionState.OutOfEntity || this.State == EntityConnection.ConnectionState.Connecting || this.State == EntityConnection.ConnectionState.Finding || this.State == EntityConnection.ConnectionState.Binding)
				{
					this.DoClose();
					return;
				}
				if (this.State == EntityConnection.ConnectionState.Bound || this.State == EntityConnection.ConnectionState.Initial)
				{
					this.SendMessage(new RequestClose
					{
						IsForced = isForced
					});
				}
			}
		}

		private void DoClose()
		{
			if (this.Proxy != null)
			{
				this.Proxy.Dispose();
				this.Proxy = null;
			}
			while (this.OperationQueue.Count > 0)
			{
				Operation operation = this.OperationQueue.Dequeue();
				operation.IssueFailEvent();
			}
			if (this.Pipe != null && !this.Pipe.IsClosed)
			{
				this.Pipe.Closed -= this.Pipe_Closed;
				this.Pipe.SendClose();
				this.Pipe.Close();
			}
			if (this.State == EntityConnection.ConnectionState.Binding && this.ConnectionFailed != null)
			{
				try
				{
					this.ConnectionFailed(this.LocalEntity, new EventArgs<IEntityProxy>(this));
				}
				catch (Exception ex)
				{
					Log<EntityConnection>.Logger.Error("Error in ConnectionFailed event handler ", ex);
				}
			}
			this.State = EntityConnection.ConnectionState.Closed;
			this.MyEntity = null;
			if (this.Closed != null)
			{
				try
				{
					this.Closed(this.LocalEntity, new EventArgs<IEntityProxy>(this));
				}
				catch (Exception ex2)
				{
					Log<EntityConnection>.Logger.Error("Error in Closed event handler ", ex2);
				}
			}
			GC.SuppressFinalize(this);
			this.IsClosed = true;
		}

		public event EventHandler<EventArgs<IEntityProxy>> ConnectionSucceeded;

		public event EventHandler<EventArgs<IEntityProxy>> ConnectionFailed;

		public event EventHandler<EventArgs<IEntityProxy>> Closed;

		internal void OnConnectionFailed()
		{
			if (this.ConnectionFailed != null)
			{
				try
				{
					this.ConnectionFailed(this.LocalEntity, new EventArgs<IEntityProxy>(this));
				}
				catch (Exception ex)
				{
					Log<EntityConnection>.Logger.Error("Error in ConnectionFailed event handler ", ex);
				}
			}
			this.MyEntity = null;
			this.State = EntityConnection.ConnectionState.Closed;
			if (this.Closed != null)
			{
				try
				{
					this.Closed(this.LocalEntity, new EventArgs<IEntityProxy>(this));
				}
				catch (Exception ex2)
				{
					Log<EntityConnection>.Logger.Error("Error in Closed event handler ", ex2);
				}
			}
			GC.SuppressFinalize(this);
			this.IsClosed = true;
		}

		public bool AttachPipe(Pipe pipe)
		{
			if (this.Pipe != null)
			{
				Log<EntityConnection>.Logger.Error("Duplicated pipe on entityconnection");
			}
			this.Pipe = pipe;
			this.Pipe.MessageReceived += this.Pipe_MessageReceived;
			this.Pipe.Closed += this.Pipe_Closed;
			return true;
		}

		public void SendMessage(object msg)
		{
			if (this.Pipe == null)
			{
				this.MessageQueue.Enqueue(msg);
				return;
			}
			if (!this.Pipe.IsClosed)
			{
				this.Pipe.SendObject(msg);
				return;
			}
			if (Log<EntityConnection>.Logger.IsInfoEnabled)
			{
				Log<EntityConnection>.Logger.InfoFormat("try send to closed pipe : {0}", msg);
			}
		}

		internal void Flush()
		{
			while (this.MessageQueue.Count > 0)
			{
				this.SendMessage(this.MessageQueue.Dequeue());
			}
		}

		private void Pipe_Closed(Pipe pipe)
		{
			if (this.State != EntityConnection.ConnectionState.Closed)
			{
				this.DoClose();
			}
		}

		private void Pipe_MessageReceived(object message)
		{
			Action<EntityConnection, object> action;
			if (this.MessageHandlers.TryGetValue(message.GetType(), out action))
			{
				action(this, message);
				return;
			}
			if (message is Operation)
			{
				if (this.Proxy != null)
				{
					Log<EntityConnection>.Logger.ErrorFormat("Operation received on processing connection : [{0}]", this.Proxy.Processor);
					this.Proxy.Dispose();
				}
				if (this.State == EntityConnection.ConnectionState.Initial)
				{
					Log<EntityConnection>.Logger.DebugFormat("Not entity operation : {0}", message);
					this.State = EntityConnection.ConnectionState.OutOfEntity;
					this.Flush();
				}
				this.Proxy = new OperationProxy(delegate(object msg)
				{
					this.SendMessage(msg);
				}, this.ProcessorBuilder);
				this.Proxy.ProcessorAttached += this.Proxy_ProcessorAttached;
				this.Proxy.ProcessorDisposed += this.Proxy_ProcessorDisposed;
				this.Proxy.ProcessMessage(message);
				return;
			}
			if (message is Identify)
			{
				if (this.RemoteCategory != null)
				{
					Log<EntityConnection>.Logger.ErrorFormat("Duplicate identity : [{0} {1}]", this.RemoteID, this.RemoteCategory);
				}
				Identify identify = message as Identify;
				this.RemoteID = identify.ID;
				this.RemoteCategory = identify.Category;
				if (this.State == EntityConnection.ConnectionState.Binding)
				{
					this.State = EntityConnection.ConnectionState.Connected;
					this.Flush();
					if (this.ConnectionSucceeded != null)
					{
						this.ConnectionSucceeded(this.LocalEntity, new EventArgs<IEntityProxy>(this));
					}
					if (this.OperationQueue.Count > 0)
					{
						this.RequestOperation(this.OperationQueue.Dequeue());
						return;
					}
					if (this.IsClosing)
					{
						this.DoClose();
						return;
					}
				}
			}
			else
			{
				if (message is BindEntity)
				{
					if (this.LocalEntity != null)
					{
						Log<EntityConnection>.Logger.ErrorFormat("Duplicate bind entity upon [{0}({1})]", this.LocalEntity.ID, this.LocalEntity.Category);
					}
					BindEntity bindmsg = message as BindEntity;
					this.EntityMaker(bindmsg.ID, bindmsg.Category, delegate(IEntity entity, BindEntityResult result)
					{
						if (entity == null)
						{
							if (result != BindEntityResult.Fail_NoExistingEntity)
							{
								Log<EntityConnection>.Logger.ErrorFormat("Bind fail : [{0}({1}) : {2}]", bindmsg.ID, bindmsg.Category, result);
							}
							this.Close();
							return;
						}
						this.State = EntityConnection.ConnectionState.Bound;
						this.Flush();
						this.OwnerConnection = bindmsg.OwnerConnection;
						this.MyEntity = (entity as Entity);
						this.Pipe.Send<Identify>(new Identify
						{
							ID = entity.ID,
							Category = entity.Category
						});
						if (this.IsClosing)
						{
							this.SendMessage(new RequestClose());
						}
					}, bindmsg.OwnerConnection);
					return;
				}
				if (message is RequestClose)
				{
					this.Close((message as RequestClose).IsForced);
					return;
				}
				if (this.Proxy != null)
				{
					this.Proxy.ProcessMessage(message);
					return;
				}
				Log<EntityConnection>.Logger.ErrorFormat("Invalid message [{0}/{1}]", message, message.ToString());
			}
		}

		public string ReportQueueStatus()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.OperationQueue.Count != 0)
			{
				stringBuilder.AppendFormat("OPQ[{0}]", this.OperationQueue.Count);
			}
			if (this.MessageQueue.Count != 0)
			{
				stringBuilder.AppendFormat("MSGQ[{0}]", this.MessageQueue.Count);
			}
			if (this.Proxy != null)
			{
				stringBuilder.AppendFormat("Processing[{0}:{1}]", this.Proxy.Processor.Operation, this.Proxy.Processor);
			}
			return stringBuilder.ToString();
		}

		public string OperationReport()
		{
			return OperationTimeReportElement.ReportString(this.OperationTimeReport);
		}

		public void ClearOperationTimeReport()
		{
			this.OperationTimeReport.Clear();
		}

		private const int MaxOperationQueueSize = 100;

		private Entity myEntity;

		public Dictionary<string, OperationTimeReportElement> OperationTimeReport = new Dictionary<string, OperationTimeReportElement>();

		private Queue<Operation> OperationQueue = new Queue<Operation>();

		private Queue<object> MessageQueue = new Queue<object>();

		private Dictionary<Type, Action<EntityConnection, object>> MessageHandlers = new Dictionary<Type, Action<EntityConnection, object>>();

		public enum ConnectionState
		{
			Initial,
			Finding,
			Connecting,
			Binding,
			Connected,
			Bound,
			OutOfEntity,
			Closed
		}
	}
}
