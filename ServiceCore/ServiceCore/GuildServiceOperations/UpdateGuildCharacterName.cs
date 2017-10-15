using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class UpdateGuildCharacterName : Operation
	{
		public UpdateGuildCharacterName(GuildMemberKey key, string newName)
		{
			this.Key = key;
			this.NewName = newName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public GuildMemberKey Key;

		public string NewName;
	}
}
