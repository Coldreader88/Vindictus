using System;

namespace WcfChatRelay.Whisper
{
	public class WhisperResultAsyncEventArg : WhisperResultArg
	{
		public IAsyncResult AsyncResult { get; set; }

		public WhisperCompleted Callback { get; set; }
	}
}
