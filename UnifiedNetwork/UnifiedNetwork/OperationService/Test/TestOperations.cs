using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.OperationService.Test
{
	internal static class TestOperations
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
				return TestOperations.Types.GetConverter();
			}
		}
	}
}
