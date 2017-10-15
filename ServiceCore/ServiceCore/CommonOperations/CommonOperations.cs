using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CommonOperations
{
	public static class CommonOperations
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
				{
					if (!(type.Namespace != typeof(CommonOperations).Namespace) && typeof(Operation).IsAssignableFrom(type))
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
				return CommonOperations.Types.GetConverter();
			}
		}
	}
}
