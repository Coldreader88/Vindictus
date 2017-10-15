using System;

namespace WcfChatRelay.Whisper
{
	public class WhisperAsyncEventArg : WhisperEventArg
	{
		public IAsyncResult AsyncResult { get; set; }

		public WhisperCompleted Callback { get; set; }
	}
}
