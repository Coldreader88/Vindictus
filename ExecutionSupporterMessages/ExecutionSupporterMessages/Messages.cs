using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace ExecutionSupporterMessages
{
	public static class Messages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(ExecuteMessage);
				yield return typeof(ClientReportMessage);
				yield return typeof(QueryClientReportMessage);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return Messages.Types.GetConverter();
			}
		}
	}
}
