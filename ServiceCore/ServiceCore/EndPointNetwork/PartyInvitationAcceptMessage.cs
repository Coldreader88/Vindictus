using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyInvitationAcceptMessage : IMessage
	{
		public long PartyID { get; set; }

		public PartyInvitationAcceptMessage(long PartyID)
		{
			this.PartyID = PartyID;
		}

		public override string ToString()
		{
			return string.Format("PartyInvitationAcceptMessage[ PartyID = {0} ]", this.PartyID);
		}
	}
}
