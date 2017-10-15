using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	public static class RandomMissionOperations
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
				{
					if (!(type.Namespace != typeof(RandomMissionOperations).Namespace) && typeof(Operation).IsAssignableFrom(type))
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
				return RandomMissionOperations.Types.GetConverter();
			}
		}
	}
}
