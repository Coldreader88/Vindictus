using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class HousingKickMember : Operation
	{
		public long HostCID { get; set; }

		public int MemberSlot { get; set; }

		public int NexonSN { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
