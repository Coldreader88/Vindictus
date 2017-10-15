using System;
using Utility;

namespace UnifiedNetwork.CacheSync
{
	public class LocalObserver<ResultT>
	{
		public ResultT ReturnValue { get; private set; }

		public bool Dirty { get; private set; }

		public LocalObserver(IObservable observable, Func<ResultT> getResult)
		{
			this.target = observable;
			this.target.CacheUpdated += this.target_CacheUpdated;
			this.Dirty = true;
			this.GetResult = getResult;
		}

		public void Close()
		{
			this.target.CacheUpdated -= this.target_CacheUpdated;
		}

		private void target_CacheUpdated(object sender, EventArgs e)
		{
			this.Dirty = true;
		}

		public void ForceSync()
		{
			if (this.Dirty)
			{
				try
				{
					this.ReturnValue = this.GetResult();
					this.Dirty = false;
				}
				catch (Exception ex)
				{
					Log<LocalObserver<ResultT>>.Logger.Error("Error while GetResult : ", ex);
					this.Dirty = true;
				}
			}
		}

		private IObservable target;

		private Func<ResultT> GetResult;
	}
}
