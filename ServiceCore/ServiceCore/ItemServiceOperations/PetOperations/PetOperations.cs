using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	public static class PetOperations
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
				{
					if (!(type.Namespace != typeof(PetOperations).Namespace) && typeof(Operation).IsAssignableFrom(type))
					{
						yield return type;
					}
				}
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return PetOperations.Types.GetConverter();
			}
		}
	}
}
