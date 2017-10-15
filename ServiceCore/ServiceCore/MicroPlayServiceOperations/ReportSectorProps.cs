using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class ReportSectorProps : Operation
	{
		public long HostCID { get; private set; }

		public IDictionary<int, int> Props { get; private set; }

		public ReportSectorProps(long cid, IDictionary<int, int> props)
		{
			this.HostCID = cid;
			this.Props = props;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new ReportSectorProps.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(ReportSectorProps op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield break;
			}
		}
	}
}
