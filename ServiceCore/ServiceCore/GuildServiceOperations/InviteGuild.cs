using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class InviteGuild : Operation
	{
		public GuildMemberKey Key { get; set; }

		public string Name { get; set; }

		public InviteGuild(GuildMemberKey key, string name)
		{
			this.Key = key;
			this.Name = name;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
