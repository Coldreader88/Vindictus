using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using ServiceCore.CommonOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore
{
	public static class OperationTypes
	{
		public static IEnumerable<Type> Types(Func<Type, bool> isFit)
		{
			foreach (Type type in CommonOperations.CommonOperations.Types)
			{
				yield return type;
			}
			if (isFit != null)
			{
				foreach (Type type2 in Assembly.GetExecutingAssembly().GetTypes())
				{
					if (isFit(type2) && typeof(Operation).IsAssignableFrom(type2))
					{
						yield return type2;
					}
				}
			}
			yield break;
		}

		public static IDictionary<Type, int> TypeConverters(Func<Type, bool> isFit)
		{
			return OperationTypes.Types(isFit).GetConverter();
		}
	}
}
