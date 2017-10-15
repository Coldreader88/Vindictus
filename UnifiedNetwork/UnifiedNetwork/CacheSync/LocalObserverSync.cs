using System;
using UnifiedNetwork.Cooperation;
using Utility;

namespace UnifiedNetwork.CacheSync
{
	public class LocalObserverSync<ResultT> : ISynchronizable<ResultT>, ISynchronizable
	{
		public LocalObserverSync(LocalObserver<ResultT> observer)
		{
			this.observer = observer;
		}

		public ResultT ReturnValue
		{
			get
			{
				return this.observer.ReturnValue;
			}
		}

		public void OnSync()
		{
			this.observer.ForceSync();
			if (this.OnFinished != null)
			{
				try
				{
					this.OnFinished(this);
				}
				catch (Exception ex)
				{
					Log<LocalObserverSync<ResultT>>.Logger.Error("Error while OnFinished : ", ex);
				}
			}
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result
		{
			get
			{
				return !this.observer.Dirty;
			}
		}

		private LocalObserver<ResultT> observer;
	}
}
