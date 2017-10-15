using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class PvpReport : Operation
	{
		public long CID { get; set; }

		public PvpReportType Event { get; set; }

		public int Subject { get; set; }

		public int Object { get; set; }

		public string Arg { get; set; }

		public PvpReport(long cid, PvpReportType type, int subj, int obj, string arg)
		{
			this.CID = cid;
			this.Event = type;
			this.Subject = subj;
			this.Object = obj;
			this.Arg = arg;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
