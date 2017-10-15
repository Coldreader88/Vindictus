using System;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class ReportSelectMessage
	{
		public int serviceID { get; set; }

		public long entityID { get; set; }

		public ReportSelectMessage()
		{
		}

		public ReportSelectMessage(int sID, long eID)
		{
			this.serviceID = sID;
			this.entityID = eID;
		}

		public override string ToString()
		{
			return string.Format("ReportSelectMessage[{0},{1}]", this.serviceID, this.entityID);
		}
	}
}
