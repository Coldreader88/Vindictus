using System;

namespace WcfChatRelay.Whisper
{
	public class WhisperEventArg : EventArgs
	{
		public string From { get; set; }

		public long ToCID { get; set; }

		public string Message { get; set; }
	}
}
