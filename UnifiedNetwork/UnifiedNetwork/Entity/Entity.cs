using System;
using System.Collections.Generic;
using Devcat.Core;
using UnifiedNetwork.EntityGraph;
using Utility;

namespace UnifiedNetwork.Entity
{
	public class Entity : IEntity, IEntityGraphNode
	{
		public long ID { get; private set; }

		public string Category { get; private set; }

		public object Tag { get; set; }

		public T GetTag<T>()
		{
			if (this.Tag == null)
			{
				return default(T);
			}
			if (typeof(T).IsAssignableFrom(this.Tag.GetType()))
			{
				return (T)((object)this.Tag);
			}
			return default(T);
		}

		public bool IsClosing { get; set; }

		public bool IsClosed { get; set; }

		public string State
		{
			get
			{
				if (this.IsClosed)
				{
					return "Closed";
				}
				if (this.IsClosing)
				{
					return "Closing";
				}
				return "Normal";
			}
		}

		private ICollection<IEntityProxy> Callees { get; set; }

		public IEnumerable<IEntityProxy> DependsOn
		{
			get
			{
				return this.Callees;
			}
		}

		private ICollection<IEntityAdapter> Callers { get; set; }

		public IEnumerable<IEntityAdapter> UsedBy
		{
			get
			{
				return this.Callers;
			}
		}

		public int UseCount
		{
			get
			{
				return this.Callers.Count;
			}
		}

		public bool EnableAutoClose { get; set; }

		public event EventHandler Closed;

		public event EventHandler<EventArgs<IEntityAdapter>> Using;

		public event EventHandler<EventArgs<IEntityAdapter>> Used;

		public Entity(long id, string category)
		{
			this.ID = id;
			this.Category = category;
			this.Callees = new HashSet<IEntityProxy>();
			this.Callers = new HashSet<IEntityAdapter>();
			this.EnableAutoClose = true;
		}

		public Entity(long id, string category, bool enableAutoClose)
		{
			this.ID = id;
			this.Category = category;
			this.Callees = new HashSet<IEntityProxy>();
			this.Callers = new HashSet<IEntityAdapter>();
			this.EnableAutoClose = enableAutoClose;
		}

		public void Close()
		{
			this.Close(false);
		}

		public void Close(bool isForced)
		{
			if (this.IsClosed)
			{
				return;
			}
			this.IsClosing = true;
			if (0 < this.Callees.Count || 0 < this.Callers.Count)
			{
				EntityConnection[] array = new EntityConnection[this.Callees.Count + this.Callers.Count];
				this.Callees.CopyTo(array, 0);
				this.Callers.CopyTo(array, this.Callees.Count);
				foreach (EntityConnection entityConnection in array)
				{
					entityConnection.Close(isForced);
				}
				return;
			}
			this.IsClosed = true;
			if (this.Closed != null)
			{
				this.Closed(this, EventArgs.Empty);
			}
		}

		public void BindConnection(EntityConnection connection)
		{
			if (connection.IsIncoming)
			{
				if (this.Callers.Contains(connection))
				{
					return;
				}
				this.Callers.Add(connection);
			}
			else
			{
				if (this.Callees.Contains(connection))
				{
					return;
				}
				this.Callees.Add(connection);
			}
			if (connection.IsIncoming && this.Using != null)
			{
				try
				{
					this.Using(this, new EventArgs<IEntityAdapter>(connection));
				}
				catch (Exception ex)
				{
					Log<Entity>.Logger.Error("Error in ConnectionBound event handler", ex);
				}
			}
			if (this.IsClosing)
			{
				connection.Close();
			}
		}

		public void UnbindConnection(EntityConnection connection)
		{
			if (connection.IsIncoming)
			{
				if (!this.Callers.Remove(connection))
				{
					return;
				}
			}
			else if (!this.Callees.Remove(connection))
			{
				return;
			}
			if (connection.IsIncoming && this.Used != null)
			{
				try
				{
					this.Used(this, new EventArgs<IEntityAdapter>(connection));
				}
				catch (Exception ex)
				{
					Log<Entity>.Logger.Error("Error in ConnectionUnbound event handler", ex);
				}
			}
			if (this.IsClosed)
			{
				return;
			}
			if (this.IsClosing && this.Callees.Count == 0 && this.Callers.Count == 0 && this.EnableAutoClose)
			{
				this.IsClosed = true;
				if (this.Closed != null)
				{
					try
					{
						this.Closed(this, EventArgs.Empty);
					}
					catch (Exception ex2)
					{
						Log<Entity>.Logger.Error("Error in Closed event handler", ex2);
					}
				}
			}
		}

		public EntityGraphIdentifier[] ReportConnectedNodeList()
		{
			List<EntityGraphIdentifier> list = new List<EntityGraphIdentifier>();
			foreach (IEntityProxy entityProxy in this.Callees)
			{
				EntityConnection entityConnection = (EntityConnection)entityProxy;
				if (entityConnection.RemoteCategory == null)
				{
					list.Add(new EntityGraphIdentifier
					{
						category = "",
						entityID = entityConnection.RemoteID,
						states = string.Format("{0} ({1})", entityConnection.State.ToString(), entityConnection.ReportQueueStatus())
					});
				}
				else
				{
					list.Add(new EntityGraphIdentifier
					{
						category = entityConnection.RemoteCategory,
						entityID = entityConnection.RemoteID,
						states = string.Format("{0} ({1})", entityConnection.State.ToString(), entityConnection.ReportQueueStatus())
					});
				}
				if (list.Count >= EntityGraphNode.ConnectedNodeListSize)
				{
					break;
				}
			}
			foreach (IEntityAdapter entityAdapter in this.Callers)
			{
				EntityConnection entityConnection2 = (EntityConnection)entityAdapter;
				if (entityConnection2.RemoteCategory == null)
				{
					list.Add(new EntityGraphIdentifier
					{
						category = "",
						entityID = entityConnection2.RemoteID,
						states = string.Format("{0} ({1})", entityConnection2.State.ToString(), entityConnection2.ReportQueueStatus())
					});
				}
				else
				{
					list.Add(new EntityGraphIdentifier
					{
						category = entityConnection2.RemoteCategory,
						entityID = entityConnection2.RemoteID,
						states = string.Format("{0} ({1})", entityConnection2.State.ToString(), entityConnection2.ReportQueueStatus())
					});
				}
				if (list.Count >= EntityGraphNode.ConnectedNodeListSize)
				{
					break;
				}
			}
			foreach (EntityGraphIdentifier entityGraphIdentifier in list)
			{
			}
			return list.ToArray();
		}

		public EntityGraphIdentifier[] ReportConnectedNodeList(EntityGraphIdentifier target)
		{
			return null;
		}

		public EntityGraphIdentifier[] ReportConnectedNodeList(long includedEID)
		{
			return this.ReportConnectedNodeList();
		}

		public IEntityGraphNode GetNode(EntityGraphIdentifier target)
		{
			if (target.isNumeric)
			{
				return EntityGraphNode.NullNode;
			}
			foreach (IEntityProxy entityProxy in this.Callees)
			{
				if (this.Category == target.category && entityProxy.RemoteID == target.entityID)
				{
					return new DummyEntityGraphNode(target.category, entityProxy.RemoteID);
				}
			}
			foreach (IEntityAdapter entityAdapter in this.Callers)
			{
				if (this.Category == target.category && entityAdapter.RemoteID == target.entityID)
				{
					return new DummyEntityGraphNode(target.category, entityAdapter.RemoteID);
				}
			}
			return EntityGraphNode.NullNode;
		}

		public Dictionary<string, OperationTimeReportElement> OperationTimeReport
		{
			get
			{
				Dictionary<string, OperationTimeReportElement> dictionary = new Dictionary<string, OperationTimeReportElement>();
				foreach (IEntityProxy entityProxy in this.Callees)
				{
					EntityConnection entityConnection = (EntityConnection)entityProxy;
					foreach (KeyValuePair<string, OperationTimeReportElement> keyValuePair in entityConnection.OperationTimeReport)
					{
						OperationTimeReportElement operationTimeReportElement;
						if (!dictionary.TryGetValue(keyValuePair.Key, out operationTimeReportElement))
						{
							operationTimeReportElement = new OperationTimeReportElement();
						}
						operationTimeReportElement.Add(keyValuePair.Value);
						dictionary[keyValuePair.Key] = operationTimeReportElement;
					}
				}
				foreach (IEntityAdapter entityAdapter in this.Callers)
				{
					EntityConnection entityConnection2 = (EntityConnection)entityAdapter;
					foreach (KeyValuePair<string, OperationTimeReportElement> keyValuePair2 in entityConnection2.OperationTimeReport)
					{
						OperationTimeReportElement operationTimeReportElement2;
						if (!dictionary.TryGetValue(keyValuePair2.Key, out operationTimeReportElement2))
						{
							operationTimeReportElement2 = new OperationTimeReportElement();
						}
						operationTimeReportElement2.Add(keyValuePair2.Value);
						dictionary[keyValuePair2.Key] = operationTimeReportElement2;
					}
				}
				return dictionary;
			}
		}

		public string ReportOperationTimeReport(EntityGraphIdentifier target)
		{
			if (target.isNull)
			{
				return OperationTimeReportElement.ReportString(this.OperationTimeReport);
			}
			if (target.isNumeric)
			{
				return "";
			}
			foreach (IEntityProxy entityProxy in this.Callees)
			{
				EntityConnection entityConnection = (EntityConnection)entityProxy;
				if (entityConnection.RemoteCategory == target.category && entityConnection.RemoteID == target.entityID)
				{
					return entityConnection.OperationReport();
				}
			}
			foreach (IEntityAdapter entityAdapter in this.Callers)
			{
				EntityConnection entityConnection2 = (EntityConnection)entityAdapter;
				if (entityConnection2.RemoteCategory == target.category && entityConnection2.RemoteID == target.entityID)
				{
					return entityConnection2.OperationReport();
				}
			}
			return "";
		}

		public void ClearOperationTimeReport()
		{
			foreach (IEntityProxy entityProxy in this.Callees)
			{
				EntityConnection entityConnection = (EntityConnection)entityProxy;
				entityConnection.ClearOperationTimeReport();
			}
			foreach (IEntityAdapter entityAdapter in this.Callers)
			{
				EntityConnection entityConnection2 = (EntityConnection)entityAdapter;
				entityConnection2.ClearOperationTimeReport();
			}
		}

		public int ReportUnderingCounts()
		{
			return this.Callees.Count + this.Callers.Count;
		}
	}
}
