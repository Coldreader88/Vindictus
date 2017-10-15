using System;

namespace WcfChatRelay.Whisper
{
	public class WhisperResultArg : EventArgs
	{
		public long ToCID { get; set; }

		public int ResultNo { get; set; }

		public string ReceiverName { get; set; }
	}
}
