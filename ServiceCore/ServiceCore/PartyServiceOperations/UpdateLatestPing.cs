using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class UpdateLatestPing : Operation
	{
		public int SlotNum { get; set; }

		public int LatestPing { get; set; }

		public int LatestFrameRate { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
