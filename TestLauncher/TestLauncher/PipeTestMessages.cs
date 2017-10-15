using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace TestLauncher
{
	internal static class PipeTestMessages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(IntMessage);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return PipeTestMessages.Types.GetConverter();
			}
		}
	}
}
