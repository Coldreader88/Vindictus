using System;
using Nexon.Com.Group.Game.Wrapper;

namespace GuildService.API
{
	public class HeroesGuildMemberInfo
	{
		public HeroesGuildMemberInfo()
		{
		}

		public HeroesGuildMemberInfo(GroupMemberInfo info)
		{
			this.SetInfo(info.CharacterName, info.CharacterSN, (HeroesGuildUserType)info.emGroupUserType, info.GuildSN, info.Intro, info.NameInGroup, info.NexonSN);
		}

		public HeroesGuildMemberInfo(string characterName, long characterSn, HeroesGuildUserType groupUserType, int guildSn, string intro, string nameInGroup, int nexonSn)
		{
			this.SetInfo(characterName, characterSn, groupUserType, guildSn, intro, nameInGroup, nexonSn);
		}

		private void SetInfo(string characterName, long characterSn, HeroesGuildUserType groupUserType, int guildSn, string intro, string nameInGroup, int nexonSn)
		{
			this.CharacterName = characterName;
			this.CharacterSN = characterSn;
			this.emGroupUserType = groupUserType;
			this.GuildSN = guildSn;
			this.Intro = intro;
			this.NameInGroup = nameInGroup;
			this.NexonSN = nexonSn;
		}

		public string CharacterName { get; private set; }

		public long CharacterSN { get; private set; }

		public HeroesGuildUserType emGroupUserType { get; private set; }

		public int GuildSN { get; private set; }

		public string Intro { get; private set; }

		public string NameInGroup { get; private set; }

		public int NexonSN { get; private set; }
	}
}
