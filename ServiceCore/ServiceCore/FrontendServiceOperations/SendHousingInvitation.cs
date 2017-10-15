using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class SendHousingInvitation : Operation
	{
		public string HostName { get; set; }

		public long HousingID { get; set; }

		public long TargetCID { get; set; }

		public SendHousingInvitation(string hostName, long housingID, long targetCID)
		{
			this.HostName = hostName;
			this.HousingID = housingID;
			this.TargetCID = targetCID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
