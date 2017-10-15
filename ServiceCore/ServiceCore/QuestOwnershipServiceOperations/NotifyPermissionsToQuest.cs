using System;
using ServiceCore.LoginServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class NotifyPermissionsToQuest : Operation
	{
		public Permissions CharacterPermissions { get; set; }

		public NotifyPermissionsToQuest(Permissions CharacterPermissions)
		{
			this.CharacterPermissions = CharacterPermissions;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
