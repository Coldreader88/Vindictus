using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class UpdatePCRoomCount : Operation
	{
		public int PCRoomNo { get; set; }

		public int CurrentUserCount { get; set; }

		public UpdatePCRoomCount(int pcRoomNo, int currentCount)
		{
			this.PCRoomNo = pcRoomNo;
			this.CurrentUserCount = currentCount;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
