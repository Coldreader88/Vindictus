using System;

namespace ExecutionSupporterMessages
{
	[Serializable]
	public sealed class QueryClientReportMessage
	{
		public bool IsDS { get; set; }

		public QueryClientReportMessage(bool isDS)
		{
			this.IsDS = isDS;
		}
	}
}
