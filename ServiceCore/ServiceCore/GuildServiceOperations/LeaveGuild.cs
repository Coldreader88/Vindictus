using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class LeaveGuild : Operation
	{
		public LeaveGuild(GuildMemberKey key)
		{
			this.Key = key;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public GuildMemberKey Key;
	}
}
