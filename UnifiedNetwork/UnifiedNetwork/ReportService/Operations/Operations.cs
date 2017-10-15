using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.ReportService.Operations
{
	internal static class Operations
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(RequestLookUpInfo);
				yield return typeof(RequestUnderingList);
				yield return typeof(RequestOperationTimeReport);
				yield return typeof(EnableOperationTimeReport);
				yield return typeof(RequestShutDownEntity);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return Operations.Types.GetConverter(26624);
			}
		}
	}
}
