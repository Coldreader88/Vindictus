using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using ServiceCore.CharacterServiceOperations.RandomMissionOperations;
using ServiceCore.CommonOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	public static class CharacterServiceOperations
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
					if ((!(type2.Namespace != typeof(CharacterServiceOperations).Namespace) || !(type2.Namespace != typeof(RandomMissionOperations.RandomMissionOperations).Namespace)) && typeof(Operation).IsAssignableFrom(type2))
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
				return CharacterServiceOperations.Types.GetConverter();
			}
		}
	}
}
