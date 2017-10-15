using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class JoinGuildChatRoom : Operation
	{
		public GuildMemberKey Key { get; set; }

		public JoinGuildChatRoom(GuildMemberKey key)
		{
			this.Key = key;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
