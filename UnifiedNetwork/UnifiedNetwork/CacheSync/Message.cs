using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.CacheSync
{
	internal static class Message
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(AddObserver);
				yield return typeof(AddFail);
				yield return typeof(AddOk);
				yield return typeof(SetDirty);
				yield return typeof(ResetDirty);
				yield return typeof(StartSync);
				yield return typeof(Synchronized);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return Message.Types.GetConverter(8192);
			}
		}
	}
}
