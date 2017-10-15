using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.Entity.Test
{
	internal static class BOP
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(TestOp);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return BOP.Types.GetConverter();
			}
		}
	}
}
