using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.PipedNetwork
{
	internal static class Message
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(OpenPipe);
				yield return typeof(ClosePipe);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return Message.Types.GetConverter(28672);
			}
		}
	}
}
