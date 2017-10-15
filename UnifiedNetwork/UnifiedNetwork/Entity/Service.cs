using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Devcat.Core;
using Devcat.Core.Threading;
using UnifiedNetwork.CacheSync;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity.Operations;
using UnifiedNetwork.Entity.Processors;
using UnifiedNetwork.EntityGraph;
using UnifiedNetwork.OperationService;
using UnifiedNetwork.PipedNetwork;
using UnifiedNetwork.Properties;
using Utility;

namespace UnifiedNetwork.Entity
{
	public abstract class Service : UnifiedNetwork.OperationService.Service, IEntityGraphNode
	{
		public static int GameCode
		{
			get
			{
				return Settings.Default.GameCode;
			}
		}

		public static int ServerCode
		{
			get
			{
				return Settings.Default.ServerCode;
			}
		}

		private void SetRawValue(ref PerformanceCounter pc, long value)
		{
			if (pc != null)
			{
				try
				{
					pc.RawValue = value;
				}
				catch
				{
					pc.Dispose();
					pc = null;
				}
			}
		}

		private long AvgExecutionTime
		{
			set
			{
				this.SetRawValue(ref this.pcs[0], value);
			}
		}

		private long AvgExecutionTimeBase
		{
			set
			{
				this.SetRawValue(ref this.bcs[0], value);
			}
		}

		private long RequestPerSecond
		{
			set
			{
				this.SetRawValue(ref this.pcs[1], value);
			}
		}

		private long ResponsePerSecond
		{
			set
			{
				this.SetRawValue(ref this.pcs[2], value);
			}
		}

		private long QueueLength
		{
			set
			{
				this.SetRawValue(ref this.pcs[3], value);
			}
		}

		private long CurrentConnections
		{
			set
			{
				this.SetRawValue(ref this.pcs[5], value);
			}
		}

		public Service()
		{
			this.lookup = new LookUp
			{
				Service = this,
				CategoryLookUp = base.LookUp,
				IDLookUp = base.LookUp
			};
			base.Initializing += delegate(object sender, EventArgs e)
			{
				base.RegisterMessage(Messages.TypeConverters);
				base.RegisterMessage(UnifiedNetwork.CacheSync.Message.TypeConverters);
				base.ProcessorBuilder.Add(typeof(SelectEntity), (Operation op) => new SelectEntityProcessor(this, op as SelectEntity));
				base.Thread.Enqueued += new EventHandler<EventArgs<IJob>>(this.Thread_Enqueued);
				base.Thread.Dequeued += new EventHandler<EventArgs<IJob>>(this.Thread_Dequeued);
				base.Thread.Done += this.Thread_Done;
			};
			base.SuffixChanged += delegate(object sender, EventArgs _)
			{
			};
			base.Disposed += delegate(object sender, EventArgs e)
			{
				foreach (int num in Enumerable.Range(0, this.pcs.Length))
				{
					using (this.pcs[num])
					{
						this.pcs[num] = null;
					}
					using (this.bcs[num])
					{
						this.bcs[num] = null;
					}
				}
			};
		}

		public IEnumerable<IEntity> Entities
		{
			get
			{
				return this.entities.Values;
			}
		}

		public IEntity GetEntityByID(long id)
		{
			Location key = new Location(id, base.Category);
			return this.entities.TryGetValue(key);
		}

		public abstract int CompareAndSwapServiceID(long entityID, string category, int beforeID);

		public int AcquireEntity(long entityID, string category, int beforeID)
		{
			return this.CompareAndSwapServiceID(entityID, category, beforeID);
		}

		protected virtual IEntity MakeEntity(long id, string category)
		{
			return new Entity(id, category);
		}

		protected virtual IEntity MakeEntity(long id, string category, bool enableAutoClose)
		{
			return new Entity(id, category, enableAutoClose);
		}

		protected virtual void BeginMakeEntity(long id, string category, Action<IEntity> callback)
		{
			IEntity obj;
			try
			{
				obj = this.MakeEntity(id, category);
			}
			catch (Exception ex)
			{
				Log<Service>.Logger.Fatal("Exception in make entity", ex);
				callback(null);
				return;
			}
			callback(obj);
		}

		public void BeginGetEntity(long id, string category, Action<IEntity, BindEntityResult> callback)
		{
			this.BeginGetEntity(id, category, callback, true);
		}

		public void BeginGetEntity(long id, string category, Action<IEntity, BindEntityResult> callback, bool makeEntity)
		{
			int num = this.AcquireEntity(id, category, -1);
			if (num != base.ID)
			{
				callback(null, BindEntityResult.Fail_InvalidServiceID);
				return;
			}
			Location location = new Location
			{
				ID = id,
				Category = category
			};
			IEntity arg;
			if (this.entities.TryGetValue(location, out arg))
			{
				callback(arg, BindEntityResult.Success_Existing);
				return;
			}
			Action<IEntity> action;
			if (this.entityMakeCallbacks.TryGetValue(location, out action))
			{
				Dictionary<Location, Action<IEntity>> dictionary;
				Location location2;
				(dictionary = this.entityMakeCallbacks)[location2 = location] = (Action<IEntity>)Delegate.Combine(dictionary[location2], new Action<IEntity>(delegate(IEntity e)
				{
					callback(e, BindEntityResult.Success_NewEntity);
				}));
				return;
			}
			if (makeEntity)
			{
				try
				{
					this.entityMakeCallbacks[location] = delegate(IEntity e)
					{
						callback(e, BindEntityResult.Success_NewEntity);
					};
					this.BeginMakeEntity(id, category, delegate(IEntity entity)
					{
						if (entity == null)
						{
							try
							{
								Action<IEntity> action2;
								if (this.entityMakeCallbacks.TryGetValue(location, out action2))
								{
									action2(null);
									this.entityMakeCallbacks.Remove(location);
								}
							}
							catch (Exception ex2)
							{
								Log<Service>.Logger.Fatal("Exception in entity make fail", ex2);
							}
							return;
						}
						entity.Closed += delegate(object sender, EventArgs e)
						{
							if (this.entities.Remove(location))
							{
								this.CurrentConnections = (long)this.entities.Count;
							}
						};
						bool flag = !this.entities.ContainsKey(location);
						this.entities[location] = entity;
						if (flag)
						{
							this.CurrentConnections = (long)this.entities.Count;
						}
						try
						{
							Action<IEntity> action3;
							if (this.entityMakeCallbacks.TryGetValue(location, out action3))
							{
								action3(entity);
								this.entityMakeCallbacks.Remove(location);
							}
						}
						catch (Exception ex3)
						{
							Log<Service>.Logger.Fatal("Exception in begin make entity", ex3);
						}
					});
					return;
				}
				catch (Exception ex)
				{
					Log<Service>.Logger.Fatal("Exception in begin make entity", ex);
					callback(null, BindEntityResult.Fail_Exception);
					return;
				}
			}
			callback(null, BindEntityResult.Fail_NoExistingEntity);
		}

		public DateTime MakeEntityID()
		{
			TimeSpan timeSpan;
			return this.MakeEntityID(out timeSpan);
		}

		public DateTime MakeEntityID(out TimeSpan ts)
		{
			long ticks = DateTime.UtcNow.Ticks;
			if (this.id < ticks)
			{
				int num = (int)((ushort)base.ID + 1);
				this.id = (ticks - (long)num | 65535L) + (long)num;
			}
			else
			{
				this.id += 65536L;
			}
			ts = new TimeSpan(this.id - ticks);
			DateTime result = new DateTime(this.id);
			return result;
		}

		public IEntityProxy Connect(IEntity entity, Location location)
		{
			return this.Connect(entity, location, true);
		}

		public IEntityProxy Connect(IEntity entity, Location location, bool ownerConn)
		{
			EntityConnection newConnection = new EntityConnection(entity as Entity, new Func<Type, Func<Operation, OperationProcessor>>(base.FindProcessor), new Action<long, string, Action<IEntity, BindEntityResult>, bool>(this.BeginGetEntity))
			{
				IsIncoming = false,
				OwnerConnection = ownerConn
			};
			newConnection.State = EntityConnection.ConnectionState.Finding;
			this.lookup.FindLocation(location, delegate(IPEndPoint endpoint)
			{
				if (endpoint == null)
				{
					newConnection.OnConnectionFailed();
					return;
				}
				newConnection.State = EntityConnection.ConnectionState.Connecting;
				this.ConnectToIP(endpoint, delegate(Peer peer)
				{
					if (peer == null)
					{
						newConnection.OnConnectionFailed();
					}
					Pipe pipe = peer.InitPipe();
					if (!newConnection.AttachPipe(pipe))
					{
						newConnection.OnConnectionFailed();
						return;
					}
					pipe.Send<Identify>(new Identify
					{
						ID = entity.ID,
						Category = entity.Category
					});
					newConnection.State = EntityConnection.ConnectionState.Binding;
					pipe.Send<BindEntity>(new BindEntity
					{
						ID = location.ID,
						Category = location.Category,
						OwnerConnection = ownerConn
					});
				});
			});
			return newConnection;
		}

		public void RequestOperation(IEntity entity, Location location, Operation op)
		{
			this.RequestOperation(entity, location, op, true);
		}

		public void RequestOperation(IEntity entity, Location location, Operation op, bool makeEntity)
		{
			IEntityProxy connection = this.Connect(entity, location, makeEntity);
			connection.ConnectionSucceeded += delegate(object sender, EventArgs<IEntityProxy> e)
			{
				connection.RequestOperation(op);
			};
			connection.ConnectionFailed += delegate(object sender, EventArgs<IEntityProxy> e)
			{
				op.IssueFailEvent();
			};
			op.OnComplete += delegate(Operation _)
			{
				connection.Close();
			};
			op.OnFail += delegate(Operation _)
			{
				connection.Close();
			};
		}

		protected override void peer_OnPipeOpen(Peer peer, Pipe pipe)
		{
			EntityConnection entityConnection = new EntityConnection(null, new Func<Type, Func<Operation, OperationProcessor>>(base.FindProcessor), new Action<long, string, Action<IEntity, BindEntityResult>, bool>(this.BeginGetEntity))
			{
				IsIncoming = true
			};
			entityConnection.AttachPipe(pipe);
		}

		private void Thread_Enqueued(object sender, EventArgs e)
		{
			this.RequestPerSecond = (this.requestPerSecond += 1L);
			this.QueueLength = Interlocked.Increment(ref this.queueLength);
		}

		private void Thread_Dequeued(object sender, EventArgs e)
		{
			if (this.stopwatch == null)
			{
				this.stopwatch = new Stopwatch();
			}
			this.stopwatch.Reset();
			this.ResponsePerSecond = (this.responsePerSecond += 1L);
			this.QueueLength = Interlocked.Decrement(ref this.queueLength);
			this.stopwatch.Start();
		}

		private void Thread_Done(object sender, EventArgs<IJob> e)
		{
			this.stopwatch.Stop();
			this.AvgExecutionTime = (this.avgExecutionTime += this.stopwatch.ElapsedTicks);
			this.AvgExecutionTimeBase = (this.avgExecutionTimeBase += 1L);
		}

		public override EntityGraphIdentifier[] ReportConnectedNodeList()
		{
			List<EntityGraphIdentifier> list = new List<EntityGraphIdentifier>();
			Log<Service>.Logger.Debug("Report undering lists.");
			foreach (KeyValuePair<Location, IEntity> keyValuePair in this.entities)
			{
				Entity entity = keyValuePair.Value as Entity;
				list.Add(new EntityGraphIdentifier
				{
					serviceID = base.ID,
					entityID = entity.ID,
					states = string.Format("{0} {1}", entity.State, (entity.Tag == null) ? "null" : entity.Tag.ToString())
				});
				if (list.Count >= EntityGraphNode.ConnectedNodeListSize)
				{
					break;
				}
			}
			return list.ToArray();
		}

		public override EntityGraphIdentifier[] ReportConnectedNodeList(long includedEID)
		{
			List<EntityGraphIdentifier> list = new List<EntityGraphIdentifier>();
			Log<Service>.Logger.Debug("Report undering lists.");
			foreach (KeyValuePair<Location, IEntity> keyValuePair in this.entities)
			{
				Entity entity = keyValuePair.Value as Entity;
				if (entity.ID == includedEID)
				{
					list.Add(new EntityGraphIdentifier
					{
						serviceID = base.ID,
						entityID = entity.ID,
						states = string.Format("{0} {1}", entity.State, (entity.Tag == null) ? "null" : entity.Tag.ToString())
					});
					break;
				}
			}
			foreach (KeyValuePair<Location, IEntity> keyValuePair2 in this.entities)
			{
				Entity entity2 = keyValuePair2.Value as Entity;
				if (list.Count < EntityGraphNode.ConnectedNodeListSize && entity2.ID != includedEID)
				{
					list.Add(new EntityGraphIdentifier
					{
						serviceID = base.ID,
						entityID = entity2.ID,
						states = string.Format("{0} {1}", entity2.State, (entity2.Tag == null) ? "null" : entity2.Tag.ToString())
					});
				}
				if (list.Count >= EntityGraphNode.ConnectedNodeListSize)
				{
					break;
				}
			}
			return list.ToArray();
		}

		public override EntityGraphIdentifier[] ReportConnectedNodeList(EntityGraphIdentifier target)
		{
			IEntityGraphNode node = this.GetNode(target);
			Log<Service>.Logger.Debug("Report undering lists.");
			return node.ReportConnectedNodeList();
		}

		public override IEntityGraphNode GetNode(EntityGraphIdentifier target)
		{
			if (target.isNumeric)
			{
				if (target.serviceID != base.ID)
				{
					return EntityGraphNode.NullNode;
				}
				if (target.entityID == (long)EntityGraphNode.ServiceEntityID)
				{
					return this;
				}
				foreach (KeyValuePair<Location, IEntity> keyValuePair in this.entities)
				{
					Entity entity = keyValuePair.Value as Entity;
					if (entity.ID == target.entityID)
					{
						return entity;
					}
				}
			}
			if (target.isCategoric)
			{
				if (target.category != base.Category)
				{
					return EntityGraphNode.NullNode;
				}
				if (target.entityID == (long)EntityGraphNode.ServiceEntityID)
				{
					return this;
				}
				foreach (KeyValuePair<Location, IEntity> keyValuePair2 in this.entities)
				{
					Entity entity2 = keyValuePair2.Value as Entity;
					if (entity2.ID == target.entityID)
					{
						return entity2;
					}
				}
			}
			return EntityGraphNode.NullNode;
		}

		public Dictionary<string, OperationTimeReportElement> OperationTimeReport
		{
			get
			{
				Dictionary<string, OperationTimeReportElement> dictionary = new Dictionary<string, OperationTimeReportElement>();
				foreach (KeyValuePair<Location, IEntity> keyValuePair in this.entities)
				{
					Entity entity = keyValuePair.Value as Entity;
					foreach (KeyValuePair<string, OperationTimeReportElement> keyValuePair2 in entity.OperationTimeReport)
					{
						OperationTimeReportElement operationTimeReportElement;
						if (!dictionary.TryGetValue(keyValuePair2.Key, out operationTimeReportElement))
						{
							operationTimeReportElement = new OperationTimeReportElement();
						}
						operationTimeReportElement.Add(keyValuePair2.Value);
						dictionary[keyValuePair2.Key] = operationTimeReportElement;
					}
				}
				return dictionary;
			}
		}

		public override string ReportOperationTimeReport(EntityGraphIdentifier p, EntityGraphIdentifier q)
		{
			IEntityGraphNode node = this.GetNode(p);
			if (node == EntityGraphNode.NullNode)
			{
				return "";
			}
			if (node is Entity)
			{
				Entity entity = node as Entity;
				Log<Service>.Logger.Debug("Found Entity : Request TimeReport");
				return entity.ReportOperationTimeReport(q);
			}
			return OperationTimeReportElement.ReportString(this.OperationTimeReport);
		}

		public override void ClearOperationTimeReport()
		{
			foreach (KeyValuePair<Location, IEntity> keyValuePair in this.entities)
			{
				Entity entity = keyValuePair.Value as Entity;
				entity.ClearOperationTimeReport();
			}
		}

		public override void ShutDownEntity(EntityGraphIdentifier p)
		{
			IEntityGraphNode node = this.GetNode(p);
			if (node == EntityGraphNode.NullNode)
			{
				return;
			}
			if (node == null)
			{
				return;
			}
			Entity entity = node as Entity;
			entity.Close();
			Scheduler.Schedule(base.Thread, Job.Create<Entity>(new Action<Entity>(this.ForceRemoveEntity), entity), 10000);
		}

		private void ForceRemoveEntity(IEntity entity)
		{
			Location key = new Location(entity.ID, entity.Category);
			IEntity entity2;
			if (this.entities.TryGetValue(key, out entity2) && entity2 == entity && this.entities.Remove(key))
			{
				this.CurrentConnections = (long)this.entities.Count;
			}
		}

		public override int ReportUnderingCounts()
		{
			return this.entities.Count;
		}

		public override long GetEntityCount()
		{
			return (long)this.entities.Count;
		}

		private PerformanceCounter[] pcs = new PerformanceCounter[6];

		private PerformanceCounter[] bcs = new PerformanceCounter[6];

		private long avgExecutionTime;

		private long avgExecutionTimeBase;

		private long requestPerSecond;

		private long responsePerSecond;

		private long queueLength;

		private LookUp lookup;

		private Dictionary<Location, IEntity> entities = new Dictionary<Location, IEntity>();

		private Dictionary<Location, Action<IEntity>> entityMakeCallbacks = new Dictionary<Location, Action<IEntity>>();

		private long id;

		[ThreadStatic]
		private Stopwatch stopwatch;
	}
}
