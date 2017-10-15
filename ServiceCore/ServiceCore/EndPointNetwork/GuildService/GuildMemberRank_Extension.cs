using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	public static class GuildMemberRank_Extension
	{
		public static bool IsMaster(this GuildMemberRank rank)
		{
			return rank == GuildMemberRank.Master;
		}

		public static bool IsOperator(this GuildMemberRank rank)
		{
			return rank == GuildMemberRank.Master || rank == GuildMemberRank.Operator;
		}

		public static bool IsRegularMember(this GuildMemberRank rank)
		{
			return rank == GuildMemberRank.Master || rank == GuildMemberRank.Operator || rank == GuildMemberRank.Member;
		}

		public static bool IsMember(this GuildMemberRank rank)
		{
			return rank == GuildMemberRank.Master || rank == GuildMemberRank.Operator || rank == GuildMemberRank.Member || rank == GuildMemberRank.Wait;
		}

		public static bool IsInvalid(this GuildMemberRank rank)
		{
			return rank == GuildMemberRank.Ignorable || rank == GuildMemberRank.Unknown;
		}
	}
}
