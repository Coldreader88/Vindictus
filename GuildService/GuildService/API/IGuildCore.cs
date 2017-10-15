using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;

namespace GuildService.API
{
	public interface IGuildCore
	{
		HeroesGuildInfo GetGuildInfo(int guildSN);

		int OpenGuild(GuildMemberKey key, string guildName, string guildID, string guildIntro);

		bool CloseGuild(GuildEntity guild, GuildMemberKey key);

		void Join(GuildEntity guild, GuildMemberKey key);

		void LeaveGuild(GuildEntity guild, GuildMemberKey key);

		bool AcceptJoin(GuildEntity guild, GuildMemberRank operatorRank, GuildMember targetMember, bool accept);

		bool ChangeMaster(GuildEntity guild, GuildMember newMaster, GuildMember oldMaster);

		bool ChangeRank(GuildEntity guild, GuildMemberRank operatorRank, GuildMember targetMember, GuildMemberRank toRank);

		List<HeroesGuildMemberInfo> GetMembers(int guildSN);

		HeroesGuildMemberInfo GetMemberInfo(GuildEntity guild, GuildMemberKey key);

		ICollection<HeroesGuildInfo> SearchGuild(int searchType, byte orderType, int pageNo, byte pageSize, string searchKey, out int total_row_count);

		List<HeroesUserGuildInfo> GetGuildInfo(GuildMemberKey key);

		bool CheckGuild(long CharacterSN, int NexonSN, string CharacterName);

		GroupNameCheckResult CheckGroupName(string guildName);

		GroupIDCheckResult CheckGroupID(string guildID);

		void UserNameModify(GuildMemberKey key, string newName);
	}
}
