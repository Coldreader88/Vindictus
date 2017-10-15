using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class QuestDigest
	{
		public string QuestID { get; private set; }

		public ICollection<GoalTarget> Essentials { get; private set; }

		public ICollection<GoalTarget> PartyBonus { get; private set; }

		public ICollection<GoalTarget> IndividualBonus { get; private set; }

		public IList<GoalTarget> Swear { get; private set; }

		public string QuestName { get; set; }

		public int QuestLevel { get; set; }

		public int QuestSet { get; set; }

		public int Period { get; set; }

		public int PartyLimit { get; set; }

		public bool HardMode { get; set; }

		public int SoloSector { get; set; }

		public bool SoloMode
		{
			get
			{
				return this.SoloSector > 0;
			}
		}

		public int SwearID
		{
			get
			{
				if (this.Swear.Count != 0)
				{
					return this.Swear[0].GoalID;
				}
				return -1;
			}
		}

		public int TimeLimit { get; set; }

		public int LastSectorTimeLimit { get; set; }

		public string ItemLimit { get; set; }

		public string GearLimit { get; set; }

		public int ItemCountMin { get; set; }

		public int ItemCountMax { get; set; }

		public string PropDropTableName { get; set; }

		public bool IsFreeQuest { get; set; }

		public bool IsHuntingQuest { get; set; }

		public bool IsSeason2 { get; set; }

		public bool DisableUserShip { get; set; }

		public float DurabilityRatio { get; set; }

		public string RequiredItem { get; set; }

		public int MinClearTime { get; set; }

		public bool EnableDSServer { get; set; }

		public string EnableDSServerConstraint { get; set; }

		public int InitGameTime { get; set; }

		public int SectorMoveGameTime { get; set; }

		public int GatheringRockMax { get; set; }

		public bool IsPracticeMode { get; set; }

		public bool IsPreUpdateQuestPlayCount
		{
			get
			{
				return this.QuestID.ToLower().CompareTo("quest_resenlian") == 0;
			}
		}

		public QuestDigest(string questid)
		{
			this.QuestID = questid;
			this.Essentials = new List<GoalTarget>();
			this.PartyBonus = new List<GoalTarget>();
			this.IndividualBonus = new List<GoalTarget>();
			this.Swear = new List<GoalTarget>();
			this.IsPracticeMode = false;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("QuestDigest(Lv{0} partyLimit {1} TimeLimit {2} LastSectorLimit {3} ItemCount{4}~{5}\n", new object[]
			{
				this.QuestLevel,
				this.PartyLimit,
				this.TimeLimit,
				this.LastSectorTimeLimit,
				this.ItemCountMin,
				this.ItemCountMax
			});
			stringBuilder.Append("HardMode ");
			if (this.HardMode)
			{
				stringBuilder.Append("On \n");
			}
			else
			{
				stringBuilder.Append("Off \n");
			}
			stringBuilder.Append("SoloMode ");
			if (this.SoloMode)
			{
				stringBuilder.Append("On \n");
			}
			else
			{
				stringBuilder.Append("Off \n");
			}
			return stringBuilder.ToString();
		}
	}
}
