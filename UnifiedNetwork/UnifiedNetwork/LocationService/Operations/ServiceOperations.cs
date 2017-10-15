using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.LocationService.Operations
{
	internal static class ServiceOperations
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(Update);
				yield return typeof(LinkedList<ServiceInfo>);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return ServiceOperations.Types.GetConverter(24576);
			}
		}
	}
}
