using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class LaunchDS : Operation
	{
		public int DSID { get; set; }

		public string QuestID { get; set; }

		public long MicroPlayID { get; set; }

		public long PartyID { get; set; }

		public long FrontendID { get; set; }

		public bool IsGiantRaid { get; set; }

		public bool IsAdultMode { get; set; }

		public LaunchDS(int dsid, string questID, long microPlayID, long partyID, long frontendID, bool isGiantRaid, bool isAdultMode)
		{
			this.DSID = dsid;
			this.QuestID = questID;
			this.MicroPlayID = microPlayID;
			this.PartyID = partyID;
			this.FrontendID = frontendID;
			this.IsGiantRaid = isGiantRaid;
			this.IsAdultMode = isAdultMode;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
