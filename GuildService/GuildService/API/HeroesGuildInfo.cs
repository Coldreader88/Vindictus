using System;
using Nexon.Com.Group.Game.Wrapper;

namespace GuildService.API
{
	public class HeroesGuildInfo
	{
		public HeroesGuildInfo()
		{
		}

		public HeroesGuildInfo(GroupInfo info)
		{
			this.SetInfo(info.dtCreateDate, info.GuildID, info.GuildName, info.GuildSN, info.Intro, info.NameInGroup_Master, info.NexonSN_Master, info.RealUserCount);
		}

		public HeroesGuildInfo(DateTime createDate, string guildId, string guildName, int guildSn, string intro, string nameInGroup_Master, int nexonSn_Master, int realUserCount)
		{
			this.SetInfo(createDate, guildId, guildName, guildSn, intro, nameInGroup_Master, nexonSn_Master, realUserCount);
		}

		private void SetInfo(DateTime createDate, string guildId, string guildName, int guildSn, string intro, string nameInGroup_Master, int nexonSn_Master, int realUserCount)
		{
			this.dtCreateDate = createDate;
			this.GuildID = guildId;
			this.GuildName = guildName;
			this.GuildSN = guildSn;
			this.Intro = intro;
			this.NameInGroup_Master = nameInGroup_Master;
			this.NexonSN_Master = nexonSn_Master;
			this.RealUserCount = realUserCount;
		}

		public int CharacterSN_Master { get; private set; }

		public DateTime dtCreateDate { get; private set; }

		public string GuildID { get; private set; }

		public string GuildName { get; private set; }

		public int GuildSN { get; private set; }

		public string Intro { get; private set; }

		public string NameInGroup_Master { get; private set; }

		public int NexonSN_Master { get; private set; }

		public int RealUserCount { get; private set; }
	}
}
