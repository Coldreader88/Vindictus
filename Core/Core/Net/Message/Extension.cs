using System;
using System.Collections.Generic;

namespace Devcat.Core.Net.Message
{
	public static class Extension
	{
		public static IDictionary<Type, int> GetConverter(this IEnumerable<Type> types)
		{
			return types.GetConverter(1);
		}

		public static IDictionary<Type, int> GetConverter(this IEnumerable<Type> types, int startCategoryId)
		{
			Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
			foreach (Type type in types)
			{
				if (type.IsSealed)
				{
					dictionary.Add(type, startCategoryId++);
				}
				else
				{
					dictionary.Add(type, 0);
				}
			}
			return dictionary;
		}
	}
}
