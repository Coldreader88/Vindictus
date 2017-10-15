using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;

namespace ServiceCore.EndPointNetwork
{
	public static class Messages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
				{
					if (type.Namespace != null && type.Namespace.StartsWith(typeof(Messages).Namespace) && !type.IsInterface && !type.IsAbstract && typeof(IMessage).IsAssignableFrom(type))
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
				return Messages.Types.GetConverter();
			}
		}
	}
}
