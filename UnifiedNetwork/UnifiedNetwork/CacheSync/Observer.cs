using System;
using Devcat.Core;
using UnifiedNetwork.Entity;
using Utility;

namespace UnifiedNetwork.CacheSync
{
	internal class Observer : IObserver
	{
		public void DebugMsg(string msg)
		{
			if (this.Target.Category == "MicroPlay" && this.Connection.LocalEntity.Category == "MicroPlayServiceCore.MicroPlayService")
			{
				Log<Observer>.Logger.FatalFormat("[{0} {1}] {2}", this.CurrentState, this.IsDirty, msg);
			}
		}

		public event Action<IObserver, Action<IObserver>> SyncBegun;

		public event Action<IObserver> Connected;

		public event Action<IObserver, Action<IObserver>> SetDirty;

		public bool IsConnected { get; private set; }

		public bool IsDirty { get; set; }

		public object Cache { get; set; }

		public Observer.State CurrentState { get; set; }

		public EntityConnection Conn { get; private set; }

		public IEntityProxy Connection
		{
			get
			{
				return this.Conn;
			}
		}

		private ObservableIdentifier Target { get; set; }

		private int ProxyID { get; set; }

		public Observer(EntityConnection connection, ObservableIdentifier target)
		{
            Observer observer = this;
			if (connection.State == EntityConnection.ConnectionState.Closed || connection.State == EntityConnection.ConnectionState.Bound)
			{
				Log<Observer>.Logger.ErrorFormat("Try to add observer to {1} connection : [{0}]", target, connection.State);
				return;
			}
			this.Conn = connection;
			this.IsDirty = true;
			this.Conn.RegisterMessageHandler(typeof(AddOk), new Action<EntityConnection, object>(this.AddOkHandler));
			this.Conn.RegisterMessageHandler(typeof(AddFail), new Action<EntityConnection, object>(this.AddFailHandler));
			this.Conn.Closed += new EventHandler<EventArgs<IEntityProxy>>(this.Connection_Closed);
			if (this.Conn.State == EntityConnection.ConnectionState.Connected)
			{
				this.Conn.SendMessage(new AddObserver(target));
			}
			else
			{
				this.Conn.ConnectionSucceeded += delegate(object sender, EventArgs<IEntityProxy> e)
				{
                    observer.Conn.SendMessage(new AddObserver(target));
				};
			}
			this.Target = target;
		}

		private void Connection_Closed(object sender, EventArgs e)
		{
			this.Close();
		}

		private void SetDirtyHandler(IEntityAdapter conn, object msg)
		{
			if (!this.IsConnected)
			{
				return;
			}
			SetDirty setDirty = msg as SetDirty;
			if (this.ProxyID != setDirty.ID)
			{
				return;
			}
			if (this.IsDirty)
			{
				Log<Observer>.Logger.Fatal("SetDirty received to dirty observer");
			}
			this.IsDirty = true;
			if (this.SetDirty != null)
			{
				this.ResetDirtyCallback = null;
				this.CurrentState = Observer.State.Synchronizing;
				this.syncSuccess = true;
				this.Conn.SendMessage(new StartSync(this.ProxyID));
				this.SetDirty(this, delegate(IObserver _)
				{
					if (this.syncSuccess)
					{
						this.Conn.SendMessage(new Synchronized(this.ProxyID));
					}
				});
			}
		}

		private void ResetDirtyHandler(IEntityAdapter conn, object msg)
		{
			if (!this.IsConnected)
			{
				return;
			}
			ResetDirty resetDirty = msg as ResetDirty;
			if (this.ProxyID != resetDirty.ID)
			{
				return;
			}
			if (!this.IsDirty)
			{
				Log<Observer>.Logger.Fatal("ResetDirty received to not dirty observer");
			}
			if (this.CurrentState != Observer.State.Synchronizing)
			{
				Log<Observer>.Logger.Fatal("ResetDirty received to not dirty observer");
			}
			this.IsDirty = false;
			this.CurrentState = Observer.State.Normal;
			if (this.ResetDirtyCallback != null)
			{
				this.ResetDirtyCallback(this);
				this.ResetDirtyCallback = null;
			}
		}

		private void AddOkHandler(IEntityAdapter conn, object msg)
		{
			AddOk addOk = msg as AddOk;
			if (!this.IsConnected && this.ProxyID == 0 && addOk.ObservableID.Equals(this.Target))
			{
				this.ProxyID = addOk.ProxyID;
				this.IsConnected = true;
				this.Conn.RegisterMessageHandler(typeof(SetDirty), new Action<EntityConnection, object>(this.SetDirtyHandler));
				this.Conn.RegisterMessageHandler(typeof(ResetDirty), new Action<EntityConnection, object>(this.ResetDirtyHandler));
				addOk.ProxyID = 0;
				addOk.ObservableID = new ObservableIdentifier(0L, "");
				if (this.Connected != null)
				{
					this.Connected(this);
				}
			}
		}

		private void AddFailHandler(IEntityAdapter conn, object msg)
		{
			AddFail addFail = msg as AddFail;
			if (!this.IsConnected && this.ProxyID == 0 && addFail.ID.Equals(this.Target))
			{
				this.Close();
				addFail.ID = new ObservableIdentifier(0L, "");
			}
		}

		public void ForceSync(Action<IObserver> callback)
		{
			if (!this.IsDirty)
			{
				callback(this);
				return;
			}
			if (this.CurrentState == Observer.State.Synchronizing)
			{
				this.ResetDirtyCallback = (Action<IObserver>)Delegate.Combine(this.ResetDirtyCallback, callback);
				return;
			}
			if (!this.IsConnected)
			{
				this.Connected += delegate(IObserver _)
				{
					this.ForceSync(callback);
				};
				return;
			}
			if (this.SyncBegun != null)
			{
				this.ResetDirtyCallback = null;
				this.CurrentState = Observer.State.Synchronizing;
				this.syncSuccess = true;
				this.Conn.SendMessage(new StartSync(this.ProxyID));
				this.SyncBegun(this, delegate(IObserver _)
				{
					if (this.syncSuccess)
					{
						Observer observer = this;
                        observer.ResetDirtyCallback = (Action<IObserver>)Delegate.Combine(observer.ResetDirtyCallback, callback);
                        observer.Conn.SendMessage(new Synchronized(this.ProxyID));
						return;
					}
					callback(this);
				});
				return;
			}
			Log<Observer>.Logger.ErrorFormat("SyncBegun not set : [{0}]", this.Conn.RemoteID, this.Conn.RemoteCategory);
			callback(null);
		}

		public void EndSync(bool success)
		{
			this.syncSuccess = success;
		}

		public void Close()
		{
			if (this.IsConnected)
			{
				this.IsConnected = false;
				this.Conn.UnregisterMessageHandler(typeof(SetDirty), new Action<EntityConnection, object>(this.SetDirtyHandler));
				this.Conn.UnregisterMessageHandler(typeof(ResetDirty), new Action<EntityConnection, object>(this.ResetDirtyHandler));
			}
			this.Conn.UnregisterMessageHandler(typeof(AddOk), new Action<EntityConnection, object>(this.AddOkHandler));
			this.Conn.UnregisterMessageHandler(typeof(AddFail), new Action<EntityConnection, object>(this.AddFailHandler));
			this.Target = new ObservableIdentifier(0L, "NONE");
			this.ProxyID = -1;
		}

		private Action<IObserver> ResetDirtyCallback;

		private bool syncSuccess;

		public enum State
		{
			Normal,
			Synchronizing
		}
	}
}
