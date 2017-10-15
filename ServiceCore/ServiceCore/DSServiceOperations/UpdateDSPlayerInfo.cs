using System;
using ServiceCore.EndPointNetwork.DS;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class UpdateDSPlayerInfo : Operation
	{
		public long CID { get; set; }

		public string QuestID { get; set; }

		public long PartyID { get; set; }

		public DSPlayerStatus Status { get; set; }

		public UpdateDSPlayerInfo(long cid, string questID, long partyid, DSPlayerStatus status)
		{
			this.CID = cid;
			this.QuestID = questID;
			this.PartyID = partyid;
			this.Status = status;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
