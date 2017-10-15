using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PlayerServiceOperations.Fishing
{
	[Serializable]
	public sealed class AddFishingResult : Operation
	{
		public string FishType { get; set; }

		public int Size { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
