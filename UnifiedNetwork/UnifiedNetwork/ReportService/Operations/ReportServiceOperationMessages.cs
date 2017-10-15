using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.ReportService.Operations
{
	public static class ReportServiceOperationMessages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(RequestLookUpInfoMessage);
				yield return typeof(ReportLookUpInfoMessage);
				yield return typeof(RequestUnderingListMessage);
				yield return typeof(ReportUnderingListMessage);
				yield return typeof(RequestSelectMessage);
				yield return typeof(ReportSelectMessage);
				yield return typeof(RequestOperationTimeReportMessage);
				yield return typeof(ReportOperationTimeReportMessage);
				yield return typeof(EnableOperationTimeReportMessage);
				yield return typeof(RequestShutDownEntityMessage);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return ReportServiceOperationMessages.Types.GetConverter(14336);
			}
		}
	}
}
