using System;
using System.Collections.Generic;
using System.Linq;
using Utility;

namespace UnifiedNetwork.CacheSync
{
	internal class Observable : IObservable
	{
		public ObservableIdentifier ID { get; set; }

		public ObservableCollection Collection { get; set; }

		public event EventHandler CacheUpdated;

		public Observable(ObservableIdentifier id, ObservableCollection collection)
		{
			this.ID = id;
			this.Collection = collection;
		}

		public void Updated()
		{
			foreach (ObserverProxy observerProxy in this.proxies.Values)
			{
				observerProxy.SetDirty();
			}
			if (this.CacheUpdated != null)
			{
				try
				{
					this.CacheUpdated(this, null);
				}
				catch (Exception ex)
				{
					Log<Observable>.Logger.Error("Exception occured while CacheUpdated : ", ex);
				}
			}
		}

		public void Close()
		{
			ObserverProxy[] array = this.proxies.Values.ToArray<ObserverProxy>();
			foreach (ObserverProxy observerProxy in array)
			{
				observerProxy.Close();
			}
			this.CacheUpdated = null;
		}

		public void AddProxy(ObserverProxy proxy)
		{
			if (this.proxies.ContainsKey(proxy.ID))
			{
				Log<Observable>.Logger.Error("Duplicated proxy");
				return;
			}
			this.proxies.Add(proxy.ID, proxy);
			this.Collection.Proxies.Add(proxy.ID, proxy);
		}

		public void RemoveProxy(ObserverProxy proxy)
		{
			ObserverProxy observerProxy;
			if (!this.proxies.TryGetValue(proxy.ID, out observerProxy) || proxy != observerProxy)
			{
				Log<Observable>.Logger.Warn("Try remove not linked proxy");
				return;
			}
			this.proxies.Remove(proxy.ID);
			this.Collection.Proxies.Remove(proxy.ID);
		}

		private Dictionary<int, ObserverProxy> proxies = new Dictionary<int, ObserverProxy>();
	}
}
