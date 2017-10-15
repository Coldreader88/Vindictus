using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class NotifyFriendInfo : Operation
	{
		public HeroesString message { get; set; }

		public SystemMessageCategory category { get; set; }

		public bool isReloadFriendList { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
