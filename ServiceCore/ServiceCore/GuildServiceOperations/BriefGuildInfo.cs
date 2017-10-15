using System;
using ServiceCore.EndPointNetwork.GuildService;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class BriefGuildInfo
	{
		public long GuildID { get; set; }

		public string GuildName { get; set; }

		public int GuildLevel { get; set; }

		public GuildMemberRank MyRank { get; set; }

		public int MaxMemberLimit { get; set; }

		public BriefGuildInfo(long guildID, string guildName, int guildLevel, GuildMemberRank rank, int maxMemberLimit)
		{
			this.GuildID = guildID;
			this.GuildName = guildName;
			this.GuildLevel = guildLevel;
			this.MyRank = rank;
			this.MaxMemberLimit = maxMemberLimit;
		}

		public static BriefGuildInfo Empty
		{
			get
			{
				if (FeatureMatrix.IsEnable("InGameGuild"))
				{
					return new BriefGuildInfo(0L, "", 0, GuildMemberRank.Unknown, FeatureMatrix.GetInteger("InGameGuild_MaxMember"));
				}
				return new BriefGuildInfo(0L, "", 0, GuildMemberRank.Unknown, 0);
			}
		}

		public bool Compare(BriefGuildInfo rhs)
		{
			return this.GuildID == rhs.GuildID && this.GuildName == rhs.GuildName && this.GuildLevel == rhs.GuildLevel && this.MyRank == rhs.MyRank;
		}
	}
}
