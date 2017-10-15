using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.LocationService.Test
{
	internal static class ABOperation
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(Plus1);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return ABOperation.Types.GetConverter();
			}
		}
	}
}
