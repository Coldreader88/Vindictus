using System;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class EnableOperationTimeReportMessage
	{
		public int serviceID { get; set; }

		public bool enable { get; set; }

		public EnableOperationTimeReportMessage()
		{
		}

		public EnableOperationTimeReportMessage(int sID, bool enabled)
		{
			this.serviceID = sID;
			this.enable = enabled;
		}

		public override string ToString()
		{
			return string.Format("EnableOperationTimeReportMessage[{0} - {1}]", this.serviceID, this.enable ? "Enable" : "Disable");
		}
	}
}
