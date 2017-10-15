using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using Nexon.CafeAuth;
using ServiceCore.CommonOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CafeAuthServiceOperations
{
	public static class MessageID
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				foreach (Type type in CommonOperations.CommonOperations.Types)
				{
					yield return type;
				}
				foreach (Type type2 in typeof(Connection).Assembly.GetTypes())
				{
					if (!type2.IsInterface && !type2.IsAbstract && !type2.IsGenericTypeDefinition && type2.IsSerializable && type2.IsSealed && type2.Namespace.StartsWith(typeof(Connection).Name))
					{
						yield return type2;
					}
				}
				foreach (Type type3 in Assembly.GetExecutingAssembly().GetTypes())
				{
					if (!(type3.Namespace != typeof(MessageID).Namespace) && typeof(Operation).IsAssignableFrom(type3))
					{
						yield return type3;
					}
				}
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return MessageID.Types.GetConverter();
			}
		}
	}
}
