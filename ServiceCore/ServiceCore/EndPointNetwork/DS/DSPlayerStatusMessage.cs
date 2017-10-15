using System;

namespace ServiceCore.EndPointNetwork.DS
{
	[Serializable]
	public sealed class DSPlayerStatusMessage : IMessage
	{
		public string QuestID { get; set; }

		public DSPlayerStatus Status { get; set; }

		public int RegisterTimeDiff { get; set; }

		public int OrderCount { get; set; }

		public int MemberCount { get; set; }

		public long PartyID { get; set; }

		public string Reason { get; set; }

		public bool IsGiantRaid { get; set; }

		public DSPlayerStatusMessage(string questID, DSPlayerStatus status, DateTime registerTime, int orderCount, bool isGiantRaid)
		{
			this.QuestID = questID;
			this.Status = status;
			this.RegisterTimeDiff = (int)(DateTime.UtcNow - registerTime).TotalSeconds;
			this.OrderCount = orderCount;
			this.MemberCount = 0;
			this.PartyID = 0L;
			this.Reason = "";
			this.IsGiantRaid = isGiantRaid;
		}

		public DSPlayerStatusMessage(string questID, DSPlayerStatus status, int memberCount, long partyID, bool isGiantRaid)
		{
			this.QuestID = questID;
			this.Status = status;
			this.RegisterTimeDiff = 0;
			this.OrderCount = 0;
			this.MemberCount = memberCount;
			this.PartyID = partyID;
			this.Reason = "";
			this.IsGiantRaid = isGiantRaid;
		}

		public DSPlayerStatusMessage(string questID, DSPlayerStatus status, long partyID, bool isGiantRaid)
		{
			this.QuestID = questID;
			this.Status = status;
			this.RegisterTimeDiff = 0;
			this.OrderCount = 0;
			this.MemberCount = 0;
			this.PartyID = partyID;
			this.Reason = "";
			this.IsGiantRaid = isGiantRaid;
		}

		public DSPlayerStatusMessage(string questID, DSPlayerStatus status, string reason, bool isGiantRaid)
		{
			this.QuestID = questID;
			this.Status = status;
			this.RegisterTimeDiff = 0;
			this.OrderCount = 0;
			this.MemberCount = 0;
			this.PartyID = 0L;
			this.Reason = reason;
			this.IsGiantRaid = isGiantRaid;
		}

		public override string ToString()
		{
			return string.Format("DSPlayerStatusMessage[ {0} {1}sec {2}th {3} ]", new object[]
			{
				this.Status,
				this.RegisterTimeDiff,
				this.OrderCount,
				this.Reason
			});
		}
	}
}
