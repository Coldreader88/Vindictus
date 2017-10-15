using System;
using GuildService.API;
using ServiceCore.GuildServiceOperations;

public class GuildOperationResult
{
	public bool Result { get; set; }

	public HeroesGuildMemberInfo HeroesGuildMemberInfo { get; set; }

	public GuildMemberKey Key { get; set; }

	public GuildOperationResult(bool result, GuildMemberKey key, HeroesGuildMemberInfo info)
	{
		this.Result = result;
		this.Key = key;
		this.HeroesGuildMemberInfo = info;
	}
}
