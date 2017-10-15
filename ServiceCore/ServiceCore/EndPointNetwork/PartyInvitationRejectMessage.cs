using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyInvitationRejectMessage : IMessage
	{
		public long PartyID { get; set; }

		public PartyInvitationRejectMessage(long PartyID)
		{
			this.PartyID = PartyID;
		}

		public override string ToString()
		{
			return string.Format("PartyInvitationRejectMessage[ PartyID = {0} ]", this.PartyID);
		}
	}
}
