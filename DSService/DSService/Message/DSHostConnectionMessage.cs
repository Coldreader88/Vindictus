using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace DSService.Message
{
	public static class DSHostConnectionMessage
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(DSHostConnectionEstablish);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return DSHostConnectionMessage.Types.GetConverter();
			}
		}
	}
}
