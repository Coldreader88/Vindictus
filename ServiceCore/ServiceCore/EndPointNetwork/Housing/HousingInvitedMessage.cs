using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingInvitedMessage : IMessage
	{
		public string HostName { get; set; }

		public long HousingID { get; set; }

		public HousingInvitedMessage(string hostName, long housingID)
		{
			this.HostName = hostName;
			this.HousingID = housingID;
		}

		public override string ToString()
		{
			return string.Format("HousingInvitedMessage[ HostName = {0} HousingID = {1} ]", this.HostName, this.HousingID);
		}
	}
}
