using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyInvitationRejectedMessage : IMessage
	{
		public string InvitedName { get; set; }

		public int Reason { get; set; }

		public PartyInvitationRejectedMessage(string InvitedName, InvitationRejectReason Reason)
		{
			this.InvitedName = InvitedName;
			this.Reason = (int)Reason;
		}

		public override string ToString()
		{
			return string.Format("PartyInvitationRejectedMessage[ InvitedName {0} Reason {1} ]", this.InvitedName, this.Reason);
		}
	}
}
