using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ShipOptionInfo
	{
		public int MaxMemberCount { get; set; }

		public int SwearMemberLimit { get; set; }

		public int UntilForceStart { get; set; }

		public int MinLevel { get; set; }

		public int MaxLevel { get; set; }

		public int IsPartyOnly { get; set; }

		public int Over18Only { get; set; }

		public int Difficulty { get; set; }

		public bool IsSeason2 { get; set; }

		public ICollection<int> SelectedBossQuestIDInfos { get; set; }

		public bool IsPracticeMode { get; set; }

		public bool UserDSMode { get; set; }

		public ShipOptionInfo(int MaxMemberCount, int SwearMemberLimit, int UntilForceStart, int MinLevel, int MaxLevel, int IsPartyOnly, int Over18Only, int Difficulty)
		{
			this.MaxMemberCount = MaxMemberCount;
			this.SwearMemberLimit = SwearMemberLimit;
			this.UntilForceStart = UntilForceStart;
			this.MinLevel = MinLevel;
			this.MaxLevel = MaxLevel;
			this.IsPartyOnly = IsPartyOnly;
			this.Over18Only = Over18Only;
			this.Difficulty = Difficulty;
			this.IsPracticeMode = false;
			this.UserDSMode = false;
		}

		public void Merge(ShipOptionInfo rhs)
		{
			if (rhs.MaxMemberCount > 0)
			{
				this.MaxMemberCount = rhs.MaxMemberCount;
			}
			if (rhs.SwearMemberLimit > 0)
			{
				this.SwearMemberLimit = rhs.SwearMemberLimit;
			}
			if (rhs.UntilForceStart > 0)
			{
				this.UntilForceStart = rhs.UntilForceStart;
			}
			if (rhs.MinLevel > 0)
			{
				this.MinLevel = rhs.MinLevel;
			}
			if (rhs.MaxLevel > 0)
			{
				this.MaxLevel = rhs.MaxLevel;
			}
			if (rhs.IsPartyOnly > 0)
			{
				this.IsPartyOnly = rhs.IsPartyOnly;
			}
			if (rhs.Over18Only > 0)
			{
				this.Over18Only = rhs.Over18Only;
			}
			if (rhs.Difficulty >= 0)
			{
				this.Difficulty = rhs.Difficulty;
			}
			this.IsSeason2 = rhs.IsSeason2;
			this.SelectedBossQuestIDInfos = rhs.SelectedBossQuestIDInfos;
			if (rhs.IsPracticeMode)
			{
				this.IsPracticeMode = true;
			}
			if (rhs.UserDSMode)
			{
				this.UserDSMode = true;
			}
		}

		public int MemberCountLimit
		{
			get
			{
				if (this.SwearMemberLimit != -1)
				{
					return Math.Min(this.SwearMemberLimit, this.MaxMemberCount);
				}
				return this.MaxMemberCount;
			}
		}
	}
}
