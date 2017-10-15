using System;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class RequestOperationTimeReportMessage
	{
		public int serviceID { get; set; }

		public long entityID { get; set; }

		public string targetCategory { get; set; }

		public long targetEntityID { get; set; }

		public RequestOperationTimeReportMessage()
		{
		}

		public RequestOperationTimeReportMessage(int sID, long eID, string tCategory, long tEID)
		{
			this.serviceID = sID;
			this.entityID = eID;
			this.targetCategory = tCategory;
			this.targetEntityID = tEID;
		}

		public override string ToString()
		{
			return string.Format("RequestOperationTimeReportMessage[{0},{1} - {2},{3}]", new object[]
			{
				this.serviceID,
				this.entityID,
				this.targetCategory,
				this.targetEntityID
			});
		}
	}
}
