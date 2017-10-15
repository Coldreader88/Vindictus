using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using ServiceCore.CommonOperations;
using ServiceCore.ItemServiceOperations.PetOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	public static class ItemServiceOperations
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
					if ((!(type2.Namespace != typeof(ItemServiceOperations).Namespace) || !(type2.Namespace != typeof(PetOperations.PetOperations).Namespace)) && typeof(Operation).IsAssignableFrom(type2))
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
				return ItemServiceOperations.Types.GetConverter();
			}
		}
	}
}
