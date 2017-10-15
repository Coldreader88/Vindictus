using System;

namespace WcfChatRelay
{
	public class WhisperAsyncResult : AsyncResult
	{
		public bool Result { get; set; }

		public WhisperAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}
	}
}
