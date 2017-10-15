using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.TalkServiceOperations
{
	[Serializable]
	public class WhisperToGameClient : Operation
	{
		public string From { get; private set; }

		public long ToCID { get; private set; }

		public string Message { get; private set; }

		public WhisperToGameClient(string from, long toCID, string message)
		{
			this.From = from;
			this.ToCID = toCID;
			this.Message = message;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
