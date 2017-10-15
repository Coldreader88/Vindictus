using System;
using System.Threading;

namespace Nexon.CafeAuth
{
	internal class AsyncResult : IAsyncResult
	{
		public AsyncResult(string nexonID, string characterID, AsyncCallback callback, object state)
		{
			this.AsyncState = state;
			this.AsyncWaitHandle = new AutoResetEvent(false);
			this.Callback = callback;
			this.NexonID = nexonID;
			this.CharacterID = characterID;
		}

		public object AsyncState { get; private set; }

		public WaitHandle AsyncWaitHandle { get; private set; }

		public bool CompletedSynchronously { get; private set; }

		public bool IsCompleted { get; private set; }

		internal AsyncCallback Callback { get; private set; }

		internal string NexonID { get; private set; }

		internal string CharacterID { get; private set; }

		internal object Response { get; set; }

		internal void Complete()
		{
			this.Complete(false);
		}

		internal void Complete(bool completedSynchronously)
		{
			this.CompletedSynchronously = completedSynchronously;
			this.IsCompleted = true;
			(this.AsyncWaitHandle as EventWaitHandle).Set();
			if (this.Callback != null)
			{
				this.Callback(this);
			}
		}
	}
}
