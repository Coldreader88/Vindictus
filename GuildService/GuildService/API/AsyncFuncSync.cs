using System;
using Devcat.Core;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;
using Utility;

namespace GuildService.API
{
	public class AsyncFuncSync<T> : ISynchronizable
	{
		private Func<T> Func { get; set; }

		private JobProcessor MainThread { get; set; }

		private JobProcessor Thread { get; set; }

		public T FuncResult { get; private set; }

		public AsyncFuncSync(Func<T> func)
		{
			this.Func = func;
			this.MainThread = JobProcessor.Current;
			this.Thread = new JobProcessor();
			this.Thread.ExceptionOccur += delegate(object s, EventArgs<Exception> e)
			{
				Log<GuildService>.Logger.Fatal("Exception occurred in AsyncFuncSync Thread", e.Value);
			};
			this.Thread.Start();
		}

		~AsyncFuncSync()
		{
			this.StopThread();
		}

		private void StopThread()
		{
			if (this.Thread != null)
			{
				this.Thread.Stop(false);
				this.Thread = null;
			}
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result { get; private set; }

		public void OnSync()
		{
			this.Thread.Enqueue(Job.Create(delegate
			{
				T funcResult;
				bool result;
				try
				{
					funcResult = this.Func();
					result = true;
				}
				catch (Exception ex)
				{
					Log<GuildService>.Logger.Error("Exception occurred in Async function call", ex);
					funcResult = default(T);
					result = false;
				}
				this.MainThread.Enqueue(Job.Create(delegate
				{
					this.FuncResult = funcResult;
					this.Result = result;
					if (this.OnFinished != null)
					{
						this.OnFinished(this);
					}
				}));
			}));
		}
	}
}
