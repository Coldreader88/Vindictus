using System;
using Nexon.Com.Group.Game.Wrapper;

namespace GuildService.API
{
	public class HeroesUserGuildInfo
	{
		public HeroesUserGuildInfo()
		{
		}

		public HeroesUserGuildInfo(UserGroupInfo info)
		{
			this.SetInfo(info.CharacterName, info.CharacterSN, info.dateCreate, (HeroesGuildUserType)info.GroupUserType, info.GuildID, info.GuildName, info.GuildSN, info.Intro, info.NameInGroup, info.NameInGroup_Master, info.NeoxnSN_Master, info.RealUserCOunt);
		}

		public HeroesUserGuildInfo(string characterName, int characterSn, DateTime dateCreate, HeroesGuildUserType groupUserType, string guildId, string guildName, int guildSn, string intro, string nameInGroup, string nameInGroup_Master, int nexonSn_Master, int realUserCount)
		{
			this.SetInfo(characterName, characterSn, dateCreate, groupUserType, guildId, guildName, guildSn, intro, nameInGroup, nameInGroup_Master, nexonSn_Master, realUserCount);
		}

		private void SetInfo(string characterName, int characterSn, DateTime dateCreate, HeroesGuildUserType groupUserType, string guildId, string guildName, int guildSn, string intro, string nameInGroup, string nameInGroup_Master, int nexonSn_Master, int realUserCount)
		{
			this.CharacterName = characterName;
			this.CharacterSN = characterSn;
			this.dateCreate = dateCreate;
			this.GroupUserType = groupUserType;
			this.GuildID = guildId;
			this.GuildName = guildName;
			this.GuildSN = guildSn;
			this.Intro = intro;
			this.NameInGroup = nameInGroup;
			this.NameInGroup_Master = nameInGroup_Master;
			this.NexonSN_Master = nexonSn_Master;
			this.RealUserCount = realUserCount;
		}

		public string CharacterName { get; private set; }

		public int CharacterSN { get; private set; }

		public DateTime dateCreate { get; private set; }

		public HeroesGuildUserType GroupUserType { get; private set; }

		public string GuildID { get; private set; }

		public string GuildName { get; private set; }

		public int GuildSN { get; private set; }

		public string Intro { get; private set; }

		public string NameInGroup { get; private set; }

		public string NameInGroup_Master { get; private set; }

		public int NexonSN_Master { get; private set; }

		public int RealUserCount { get; private set; }
	}
}
