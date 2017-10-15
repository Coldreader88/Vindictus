using System;
using System.Collections.Generic;

namespace Devcat.Core.Net.Message
{
	public class MessageSerializer
	{
		public static Packet Serialize<T>(T value)
		{
			return SerializeWriter.ToBinary<T>(value);
		}

		public static T Deserialize<T>(Packet packet)
		{
			T result;
			SerializeReader.FromBinary<T>(packet, out result);
			return result;
		}

		internal static bool IsCollection(Type type)
		{
			return Array.Exists<Type>(type.GetInterfaces(), (Type interfaceType) => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>)) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>));
		}

		internal static bool IsCustomSerializable(Type type)
		{
			return Array.IndexOf<Type>(type.GetInterfaces(), typeof(ICustomSerializable)) != -1 || type == typeof(ICustomSerializable);
		}

		internal static int GetPrimitiveSize(Type type)
		{
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (type == typeof(bool) || type == typeof(sbyte) || type == typeof(byte))
			{
				return 1;
			}
			if (type == typeof(short) || type == typeof(ushort) || type == typeof(char))
			{
				return 2;
			}
			if (type == typeof(int) || type == typeof(uint) || type == typeof(float))
			{
				return 4;
			}
			if (type == typeof(long) || type == typeof(ulong) || type == typeof(double))
			{
				return 8;
			}
			return 0;
		}
	}
}
