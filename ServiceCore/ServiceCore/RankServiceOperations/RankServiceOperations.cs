using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using ServiceCore.CommonOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	public static class RankServiceOperations
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
				{
					if ((!(type.Namespace != typeof(RankServiceOperations).Namespace) || !(type.Namespace != typeof(CommonOperations.CommonOperations).Namespace)) && typeof(Operation).IsAssignableFrom(type))
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
				return RankServiceOperations.Types.GetConverter();
			}
		}
	}
}
