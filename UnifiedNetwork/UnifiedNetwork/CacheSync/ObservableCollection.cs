using System;
using System.Collections.Generic;
using Devcat.Core;
using UnifiedNetwork.Entity;
using Utility;

namespace UnifiedNetwork.CacheSync
{
	public class ObservableCollection
	{
		internal Dictionary<int, ObserverProxy> Proxies
		{
			get
			{
				return this.proxies;
			}
		}

		public void BindEntity(IEntity entity)
		{
			entity.Using += this.entity_ConnectionBound;
            UnifiedNetwork.Entity.Entity entity2 = entity as UnifiedNetwork.Entity.Entity;
			foreach (IEntityAdapter value in entity2.UsedBy)
			{
				this.entity_ConnectionBound(entity2, new EventArgs<IEntityAdapter>(value));
			}
		}

		public void CloseAll()
		{
			foreach (Observable observable in this.observables.Values)
			{
				observable.Close();
			}
			this.observables.Clear();
		}

		public IObservable AddObservable(ObservableIdentifier id)
		{
			Observable observable = new Observable(id, this);
			this.observables.Add(id, observable);
			return observable;
		}

		public IObservable AddObservable(long id, string category)
		{
			return this.AddObservable(new ObservableIdentifier(id, category));
		}

		public bool RemoveObservable(ObservableIdentifier id)
		{
			return this.observables.Remove(id);
		}

		public bool RemoveObservable(long id, string category)
		{
			return this.RemoveObservable(new ObservableIdentifier(id, category));
		}

		private void entity_ConnectionBound(object sender, EventArgs<IEntityAdapter> e)
		{
			EntityConnection entityConnection = e.Value as EntityConnection;
			entityConnection.RegisterMessageHandler(typeof(AddObserver), new Action<EntityConnection, object>(this.AddObserverHandler));
			entityConnection.RegisterMessageHandler(typeof(StartSync), new Action<EntityConnection, object>(this.StartSyncHandler));
			entityConnection.RegisterMessageHandler(typeof(Synchronized), new Action<EntityConnection, object>(this.SynchronizedHandler));
		}

		private void StartSyncHandler(EntityConnection conn, object msg)
		{
			StartSync startSync = msg as StartSync;
			int id = startSync.ID;
			ObserverProxy observerProxy;
			if (this.proxies.TryGetValue(id, out observerProxy) && observerProxy.Connection == conn)
			{
				observerProxy.StartSyncHandler(conn, msg);
				return;
			}
			Log<ObservableCollection>.Logger.Error("not matching message StartSync");
		}

		private void SynchronizedHandler(EntityConnection conn, object msg)
		{
			Synchronized synchronized = msg as Synchronized;
			int id = synchronized.ID;
			ObserverProxy observerProxy;
			if (this.proxies.TryGetValue(id, out observerProxy) && observerProxy.Connection == conn)
			{
				observerProxy.SynchronizedHandler(conn, msg);
				return;
			}
			Log<ObservableCollection>.Logger.Error("not matching message Syncronized");
		}

		private void AddObserverHandler(EntityConnection conn, object msg)
		{
			AddObserver addObserver = msg as AddObserver;
			Observable observable;
			if (!this.observables.TryGetValue(addObserver.ID, out observable))
			{
				Log<ObservableCollection>.Logger.WarnFormat("No observable : [{0}]", addObserver.ID);
				conn.SendMessage(new AddFail(addObserver.ID));
				return;
			}
			ObserverProxy newProxy = new ObserverProxy(this.ProxyIDIssuance.ReserveNext(), observable, conn);
			observable.AddProxy(newProxy);
			conn.Closed += delegate(object sender, EventArgs<IEntityProxy> e)
			{
				newProxy.Close();
			};
			conn.SendMessage(new AddOk(observable.ID, newProxy.ID));
		}

		private Dictionary<ObservableIdentifier, Observable> observables = new Dictionary<ObservableIdentifier, Observable>();

		private IDIssuance ProxyIDIssuance = new IDIssuance(1, 2147483646);

		private Dictionary<int, ObserverProxy> proxies = new Dictionary<int, ObserverProxy>();
	}
}
