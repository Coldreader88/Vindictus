using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class HideCostume : Operation
	{
		public bool HideHead { get; set; }

		public int AvatarPart { get; set; }

		public int AvatarState { get; set; }

		public HideCostume(bool hideHead)
		{
			this.HideHead = hideHead;
			this.AvatarPart = -1;
			this.AvatarState = -1;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
