using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.CacheSync
{
	public class ObserverSync : ISynchronizable
	{
		public IObserver Observer { get; private set; }

		public ObserverSync(IObserver observer)
		{
			this.Observer = observer;
		}

		public void OnSync()
		{
			this.Observer.ForceSync(delegate(IObserver _)
			{
				this.Result = !this.Observer.IsDirty;
				this.OnFinished(this);
			});
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result { get; set; }
	}
}
