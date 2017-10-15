using System;
using Devcat.Core.Threading;

namespace UnifiedNetwork.Cooperation
{
	public class AsyncResultSync : ISynchronizable
	{
		public AsyncResultSync(JobProcessor thread)
		{
			this.thread = thread;
		}

		public void OnSync()
		{
			Scheduler.Schedule(this.thread, Job.Create(delegate
			{
				if (this.AsyncResult == null || !this.AsyncResult.IsCompleted)
				{
					return;
				}
				if (this.OnFinished != null)
				{
					this.OnFinished(this);
				}
			}), 0);
		}

		public event Action<ISynchronizable> OnAsyncCallback;

		public event Action<ISynchronizable> OnFinished;

		public bool Result { get; private set; }

		public IAsyncResult AsyncResult { get; private set; }

		public void AsyncCallback(IAsyncResult ar)
		{
			this.Result = (ar != null && ar.IsCompleted);
			this.AsyncResult = ar;
			if (this.OnAsyncCallback != null)
			{
				this.OnAsyncCallback(this);
			}
			Scheduler.Schedule(this.thread, Job.Create(delegate
			{
				if (this.OnFinished != null)
				{
					this.OnFinished(this);
				}
			}), 0);
		}

		private JobProcessor thread;
	}
}
