using System;
using UnifiedNetwork.Entity;
using Utility;

namespace UnifiedNetwork.CacheSync
{
	internal class ObserverProxy
	{
		public int ID { get; private set; }

		public Observable Parent { get; private set; }

		public EntityConnection Connection { get; private set; }

		public bool Dirty { get; private set; }

		public ObserverProxy.State CurrentState { get; set; }

		public void DebugMsg(string msg)
		{
			if (this.Parent.ID.Category == "MicroPlay" && this.Connection.RemoteCategory == "MicroPlayServiceCore.MicroPlayService")
			{
				Log<Observable>.Logger.FatalFormat("[{0} {1}] {2}", this.CurrentState, this.Dirty, msg);
			}
		}

		public ObserverProxy(int id, Observable parent, EntityConnection conn)
		{
			this.ID = id;
			this.Parent = parent;
			this.Connection = conn;
			this.Dirty = true;
		}

		internal void StartSyncHandler(IEntityAdapter conn, object msg)
		{
			if (this.CurrentState != ObserverProxy.State.Normal)
			{
				Log<ObserverProxy>.Logger.FatalFormat("StartSync received on Proxy state {0}", this.CurrentState);
			}
			this.CurrentState = ObserverProxy.State.Synchronizing;
		}

		internal void SynchronizedHandler(IEntityAdapter conn, object msg)
		{
			if (this.CurrentState != ObserverProxy.State.Synchronizing)
			{
				Log<ObserverProxy>.Logger.FatalFormat("Synchronized received on Proxy state {0}", this.CurrentState);
			}
			this.Dirty = false;
			this.CurrentState = ObserverProxy.State.Normal;
			this.Connection.SendMessage(new ResetDirty(this.ID));
		}

		public void SetDirty()
		{
			if (this.Dirty)
			{
				return;
			}
			this.Dirty = true;
			this.Connection.SendMessage(new SetDirty(this.ID));
		}

		public void Close()
		{
			this.Parent.RemoveProxy(this);
		}

		public enum State
		{
			Normal,
			Synchronizing
		}
	}
}
