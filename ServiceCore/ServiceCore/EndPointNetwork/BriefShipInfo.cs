using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BriefShipInfo
	{
		public long PartyID { get; set; }

		public string ShipName { get; set; }

		public string QuestID { get; set; }

		public int MemberCount { get; set; }

		public int MaxMemberCount { get; set; }

		public byte State { get; set; }

		public bool IsReturn { get; set; }

		public override string ToString()
		{
			return string.Format("{0}({1}, {2}/{3}, State {4})", new object[]
			{
				this.ShipName,
				this.QuestID,
				this.MemberCount,
				this.MaxMemberCount,
				this.State
			});
		}
	}
}
