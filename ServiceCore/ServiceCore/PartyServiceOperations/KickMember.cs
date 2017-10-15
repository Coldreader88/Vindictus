using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class KickMember : Operation
	{
		public long MasterCID { get; set; }

		public int MemberSlot { get; set; }

		public int NexonSN { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
