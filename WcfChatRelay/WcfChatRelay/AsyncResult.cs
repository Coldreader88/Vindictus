using System;
using System.Threading;

namespace WcfChatRelay
{
	public class AsyncResult : IAsyncResult, IDisposable
	{
		public AsyncResult(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.state = state;
			this.manualResentEvent = new ManualResetEvent(false);
		}

		object IAsyncResult.AsyncState
		{
			get
			{
				return this.state;
			}
		}

		public ManualResetEvent AsyncWait
		{
			get
			{
				return this.manualResentEvent;
			}
		}

		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				return this.AsyncWait;
			}
		}

		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		bool IAsyncResult.IsCompleted
		{
			get
			{
				return this.manualResentEvent.WaitOne(0, false);
			}
		}

		public void Complete()
		{
			this.manualResentEvent.Set();
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		public void Dispose()
		{
			this.manualResentEvent.Close();
			this.manualResentEvent = null;
			this.state = null;
			this.callback = null;
		}

		private AsyncCallback callback;

		private object state;

		private ManualResetEvent manualResentEvent;
	}
}
