using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class PvpChat : Operation
	{
		public long CID { get; set; }

		public string Message { get; set; }

		public bool IsTeamChat { get; set; }

		public PvpChat(long cid, string message, bool isTeamChat)
		{
			this.CID = cid;
			this.Message = message;
			this.IsTeamChat = isTeamChat;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
