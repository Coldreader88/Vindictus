using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.Entity.Operations
{
	internal static class Messages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(BindEntity);
				yield return typeof(SelectEntity);
				yield return typeof(Identify);
				yield return typeof(RequestClose);
				yield return typeof(SelectEntity.ResultCode);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return Messages.Types.GetConverter(20480);
			}
		}
	}
}
