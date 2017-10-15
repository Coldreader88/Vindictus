using System;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class RequestShutDownEntityMessage
	{
		public int serviceID { get; set; }

		public long entityID { get; set; }

		public RequestShutDownEntityMessage()
		{
		}

		public RequestShutDownEntityMessage(int sID, long eID)
		{
			this.serviceID = sID;
			this.entityID = eID;
		}

		public override string ToString()
		{
			return string.Format("RequestShutDownEntityMessage[{0},{1}]", this.serviceID, this.entityID);
		}
	}
}
