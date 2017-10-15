using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace AdminClientServiceCore.Messages
{
	public static class AdminClientServiceOperationMessages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(AdminRequestConsoleCommandMessage);
				yield return typeof(AdminReportNotifyMessage);
				yield return typeof(AdminRequestClientCountMessage);
				yield return typeof(AdminReportClientcountMessage);
				yield return typeof(AdminRequestServerStartMessage);
				yield return typeof(AdminRequestNotifyMessage);
				yield return typeof(AdminRequestKickMessage);
				yield return typeof(AdminRequestFreeTokenMessage);
				yield return typeof(AdminRequestShutDownMessage);
				yield return typeof(AdminRequestEmergencyStopMessage);
				yield return typeof(AdminReportEmergencyStopMessage);
				yield return typeof(AdminBroadcastConsoleCommandMessage);
				yield return typeof(AdminItemFestivalEventMessage);
				yield return typeof(AdminRequestDSChetToggleMessage);
				yield return typeof(AdminRequestClientCountMessage2);
				yield return typeof(AdminReportClientCountMessage2);
				yield return typeof(AdminEntendCashItemExpire);
				yield return typeof(AdminItemFestivalEventMessage2);
				yield return typeof(DSReportMessage);
				yield return typeof(AdminItemFestivalEventMessage3);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return AdminClientServiceOperationMessages.Types.GetConverter(14592);
			}
		}
	}
}
