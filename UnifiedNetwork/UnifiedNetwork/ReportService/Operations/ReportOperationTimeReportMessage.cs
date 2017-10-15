using System;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class ReportOperationTimeReportMessage
	{
		public string ReportString
		{
			get
			{
				return this.report;
			}
		}

		public ReportOperationTimeReportMessage()
		{
			this.report = "(No Report)";
		}

		public ReportOperationTimeReportMessage(string reportstring)
		{
			if (reportstring == "")
			{
				this.report = "(No Report)";
				return;
			}
			this.report = reportstring;
		}

		public override string ToString()
		{
			return "ReportOperationTimeReportMessage[ ]";
		}

		private string report;
	}
}
