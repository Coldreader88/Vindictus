using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.LocationService.Operations
{
	internal static class Operations
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(Register);
				yield return typeof(Query);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return Operations.Types.GetConverter();
			}
		}
	}
}
