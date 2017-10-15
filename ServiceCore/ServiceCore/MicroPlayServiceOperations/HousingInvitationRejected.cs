using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class HousingInvitationRejected : Operation
	{
		public long InvitedCID { get; set; }

		public string InvitedName { get; set; }

		public InvitationRejectReason Reason { get; set; }

		public HousingInvitationRejected(long invitedCID, string invitedName, InvitationRejectReason reason)
		{
			this.InvitedCID = invitedCID;
			this.InvitedName = invitedName;
			this.Reason = reason;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
