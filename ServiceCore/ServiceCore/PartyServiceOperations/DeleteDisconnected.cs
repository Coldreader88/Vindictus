using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class DeleteDisconnected : Operation
	{
		public long CID { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
