using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class InGameGuildInfo
	{
		public int GuildSN { get; set; }

		public string GuildName { get; set; }

		public int GuildLevel { get; set; }

		public int MemberCount { get; set; }

		public string MasterName { get; set; }

		public int MaxMemberCount { get; set; }

		public bool IsNewbieRecommend { get; set; }

		public long GuildPoint { get; set; }

		public string GuildNotice { get; set; }

		public Dictionary<byte, int> DailyGainGP { get; set; }

		public InGameGuildInfo(InGameGuildInfo rhs)
		{
			this.GuildSN = rhs.GuildSN;
			this.GuildName = rhs.GuildName;
			this.GuildLevel = rhs.GuildLevel;
			this.MemberCount = rhs.MemberCount;
			this.MasterName = rhs.MasterName;
			this.MaxMemberCount = rhs.MaxMemberCount;
			this.IsNewbieRecommend = rhs.IsNewbieRecommend;
			this.GuildPoint = rhs.GuildPoint;
			this.GuildNotice = rhs.GuildNotice;
			this.DailyGainGP = rhs.DailyGainGP;
		}

		public InGameGuildInfo(int guildSN, string guildName, int guildLevel, int memberCount, string masterName, int maxMemberCount)
		{
			this.GuildSN = guildSN;
			this.GuildName = guildName;
			this.GuildLevel = guildLevel;
			this.MemberCount = memberCount;
			this.MasterName = masterName;
			this.MaxMemberCount = maxMemberCount;
			this.IsNewbieRecommend = false;
			this.GuildPoint = 0L;
			this.GuildNotice = "";
			this.DailyGainGP = new Dictionary<byte, int>();
		}

		public InGameGuildInfo(int guildSN, string guildName, int guildLevel, int memberCount, string masterName, int maxMemberCount, bool newbieRecommend, long guildPoint, string guildNotice, Dictionary<byte, int> dailyGainGP)
		{
			this.GuildSN = guildSN;
			this.GuildName = guildName;
			this.GuildLevel = guildLevel;
			this.MemberCount = memberCount;
			this.MasterName = masterName;
			this.MaxMemberCount = maxMemberCount;
			this.IsNewbieRecommend = newbieRecommend;
			this.GuildPoint = guildPoint;
			this.GuildNotice = guildNotice;
			this.DailyGainGP = dailyGainGP;
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", new object[]
			{
				this.GuildSN,
				this.GuildName,
				this.GuildLevel,
				this.MemberCount,
				this.MasterName,
				this.MaxMemberCount,
				this.IsNewbieRecommend,
				this.GuildPoint
			});
		}
	}
}
