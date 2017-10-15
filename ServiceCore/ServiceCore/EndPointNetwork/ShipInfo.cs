using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ShipInfo
	{
		public long PartyID { get; set; }

		public string ShipName { get; set; }

		public string Password { get; set; }

		public int MinLevelConstraint { get; set; }

		public int MaxLevelConstraint { get; set; }

		public int MemberCount { get; set; }

		public int MaxShipMemberCount { get; set; }

		public string QuestID { get; set; }

		public bool IsHuntingQuest { get; set; }

		public bool IsGiantRaid { get; set; }

		public int SwearID { get; set; }

		public int RestTime { get; set; }

		public bool ReinforceAllowed { get; set; }

		public bool AdultRule { get; set; }

		public ICollection<PartyMemberInfo> Members { get; set; }

		public PartyInfoState State { get; set; }

		public int PartyBonusCount { get; set; }

		public int PartyBonusRatio { get; set; }

		public bool IsPartyOnly { get; set; }

		public int HostPing { get; set; }

		public int HostFrameRate { get; set; }

		public int Difficulty { get; set; }

		public bool IsReturn { get; set; }

		public bool IsSeason2 { get; set; }

		public ICollection<int> selectedBossQuestIDInfos { get; set; }

		public bool IsPracticeMode { get; set; }

		public bool UserDSMode { get; set; }

		public LinkedListNode<ShipInfo> ShipListNode
		{
			get
			{
				return this.shipListNode;
			}
			set
			{
				this.shipListNode = value;
			}
		}

		public override string ToString()
		{
			new StringBuilder("ShipInfo");
			return string.Format("{0}[ questId = {1} member x {2} {3}]", new object[]
			{
				this.ShipName,
				this.QuestID,
				this.MemberCount,
				this.State
			});
		}

		[NonSerialized]
		private LinkedListNode<ShipInfo> shipListNode;
	}
}
