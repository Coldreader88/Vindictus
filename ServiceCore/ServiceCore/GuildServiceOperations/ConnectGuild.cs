using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class ConnectGuild : Operation
	{
		public ConnectGuild(GuildMemberKey key)
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
