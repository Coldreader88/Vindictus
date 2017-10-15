using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingInvitationRejectMessage : IMessage
	{
		public long HousingID { get; set; }

		public HousingInvitationRejectMessage(long housingID)
		{
			this.HousingID = housingID;
		}

		public override string ToString()
		{
			return string.Format("PartyInvitationRejectMessage[ HousingID = {0} ]", this.HousingID);
		}
	}
}
