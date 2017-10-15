using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class InvitationRejected : Operation
	{
		public long InvitedCID { get; set; }

		public string InvitedName { get; set; }

		public InvitationRejectReason Reason { get; set; }

		public InvitationRejected()
		{
		}

		public InvitationRejected(long InvitedCID, string InvitedName, InvitationRejectReason reason)
		{
			this.InvitedCID = InvitedCID;
			this.InvitedName = InvitedName;
			this.Reason = reason;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
