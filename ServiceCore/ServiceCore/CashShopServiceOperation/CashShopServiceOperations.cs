using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using ServiceCore.CommonOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	public static class CashShopServiceOperations
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				foreach (Type type in CommonOperations.CommonOperations.Types)
				{
					yield return type;
				}
				foreach (Type type2 in Assembly.GetExecutingAssembly().GetTypes())
				{
					if (!(type2.Namespace != typeof(CashShopServiceOperations).Namespace) && typeof(Operation).IsAssignableFrom(type2))
					{
						yield return type2;
					}
				}
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return CashShopServiceOperations.Types.GetConverter();
			}
		}
	}
}
