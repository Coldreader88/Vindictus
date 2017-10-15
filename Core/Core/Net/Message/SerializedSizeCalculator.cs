using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Devcat.Core.Net.Message
{
	internal class SerializedSizeCalculator
	{
		public static int Calculate<T>(T value)
		{
			return SerializedSizeCalculatorHelper<T, SerializedSizeCalculator>.CalculateSerializedSize(value);
		}

		private static int CalculateSizeCount(int count)
		{
			if (count <= 65535)
			{
				if (count <= 127)
				{
					return 1;
				}
				if (count > 2047)
				{
					return 3;
				}
				return 2;
			}
			else
			{
				if (count <= 2097151)
				{
					return 4;
				}
				if (count > 67108863)
				{
					return 6;
				}
				return 5;
			}
		}

		private static int CalculateSize(IntPtr value)
		{
			return 8;
		}

		private static int CalculateSize(string value)
		{
			if (value == null)
			{
				return 1;
			}
			int byteCount = Encoding.UTF8.GetByteCount(value);
			return SerializedSizeCalculator.CalculateSizeCount(byteCount + 1) + byteCount;
		}

		private static int CalculateSize(Type value)
		{
			if (value == null)
			{
				return SerializedSizeCalculatorHelper<Guid, SerializedSizeCalculator>.CalculateSerializedSize(Guid.Empty);
			}
			if (value.IsArray)
			{
				int num = SerializedSizeCalculatorHelper<Guid, SerializedSizeCalculator>.CalculateSerializedSize(SerializedSizeCalculator.arrayGuid);
				num += SerializedSizeCalculator.CalculateSize(value.GetElementType());
				return num + SerializedSizeCalculator.CalculateSizeCount(value.Name.EndsWith("[]") ? 0 : value.GetArrayRank());
			}
			if (value.IsGenericType)
			{
				int num2 = SerializedSizeCalculatorHelper<Guid, SerializedSizeCalculator>.CalculateSerializedSize(value.GetGenericTypeDefinition().GUID);
				foreach (Type value2 in value.GetGenericArguments())
				{
					num2 += SerializedSizeCalculator.CalculateSize(value2);
				}
				return num2;
			}
			return SerializedSizeCalculatorHelper<Guid, SerializedSizeCalculator>.CalculateSerializedSize(value.GUID);
		}

		private static int CalculateSize<T>(T? value) where T : struct
		{
			int num = 1;
			if (value != null)
			{
				num += SerializedSizeCalculatorHelper<T, SerializedSizeCalculator>.CalculateSerializedSize(value.Value);
			}
			return num;
		}

		private static int CalculateSize(ArraySegment<sbyte> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<byte> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<short> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 2;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<ushort> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 2;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<int> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 4;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<uint> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 4;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<long> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 8;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<ulong> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 8;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<char> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 2;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<float> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 4;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<double> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 8;
			}
			return 1;
		}

		private static int CalculateSize(ArraySegment<bool> value)
		{
			if (value.Array != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count;
			}
			return 1;
		}

		private static int CalculateSize<T>(ArraySegment<T> value)
		{
			int num = SerializedSizeCalculator.CalculateSizeCount(value.Count + 1);
			int i = value.Offset;
			int num2 = value.Offset + value.Count;
			while (i < num2)
			{
				num += SerializedSizeCalculatorHelper<T, SerializedSizeCalculator>.CalculateSerializedSize(value.Array[i]);
				i++;
			}
			return num;
		}

		private int CalculateSize(ICollection values)
		{
			if (values == null)
			{
				return 1;
			}
			int num = SerializedSizeCalculator.CalculateSizeCount(values.Count + 1);
			foreach (object value in values)
			{
				num += SerializedSizeCalculatorHelper<object, SerializedSizeCalculator>.CalculateSerializedSize(value);
			}
			return num;
		}

		private static int CalculateSize(ICollection<sbyte> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<byte> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<short> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 2;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<ushort> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 2;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<int> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 4;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<uint> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 4;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<long> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 8;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<ulong> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 8;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<char> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 2;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<float> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 4;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<double> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 8;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<bool> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count;
			}
			return 1;
		}

		private static int CalculateSize(ICollection<string> values)
		{
			if (values == null)
			{
				return 1;
			}
			int num = SerializedSizeCalculator.CalculateSizeCount(values.Count + 1);
			foreach (string value in values)
			{
				num += SerializedSizeCalculator.CalculateSize(value);
			}
			return num;
		}

		private static int CalculateSize(ICollection<Type> values)
		{
			if (values == null)
			{
				return 1;
			}
			int num = SerializedSizeCalculator.CalculateSizeCount(values.Count + 1);
			foreach (Type value in values)
			{
				num += SerializedSizeCalculator.CalculateSize(value);
			}
			return num;
		}

		private static int CalculateSize<T>(ICollection<T> values)
		{
			if (values == null)
			{
				return 1;
			}
			int num = SerializedSizeCalculator.CalculateSizeCount(values.Count + 1);
			foreach (T value in values)
			{
				num += SerializedSizeCalculatorHelper<T, SerializedSizeCalculator>.CalculateSerializedSize(value);
			}
			return num;
		}

		private static int CalculateSize(IList<sbyte> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count;
			}
			return 1;
		}

		private static int CalculateSize(IList<byte> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count;
			}
			return 1;
		}

		private static int CalculateSize(IList<short> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 2;
			}
			return 1;
		}

		private static int CalculateSize(IList<ushort> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 2;
			}
			return 1;
		}

		private static int CalculateSize(IList<int> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 4;
			}
			return 1;
		}

		private static int CalculateSize(IList<uint> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 4;
			}
			return 1;
		}

		private static int CalculateSize(IList<long> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 8;
			}
			return 1;
		}

		private static int CalculateSize(IList<ulong> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 8;
			}
			return 1;
		}

		private static int CalculateSize(IList<char> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 2;
			}
			return 1;
		}

		private static int CalculateSize(IList<float> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 4;
			}
			return 1;
		}

		private static int CalculateSize(IList<double> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count * 8;
			}
			return 1;
		}

		private static int CalculateSize(IList<bool> value)
		{
			if (value != null)
			{
				return SerializedSizeCalculator.CalculateSizeCount(value.Count + 1) + value.Count;
			}
			return 1;
		}

		private static int CalculateSize(IList<string> value)
		{
			if (value == null)
			{
				return 1;
			}
			int num = SerializedSizeCalculator.CalculateSizeCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				num += SerializedSizeCalculator.CalculateSize(value[i]);
				i++;
			}
			return num;
		}

		private static int CalculateSize(IList<Type> value)
		{
			if (value == null)
			{
				return 1;
			}
			int num = SerializedSizeCalculator.CalculateSizeCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				num += SerializedSizeCalculator.CalculateSize(value[i]);
				i++;
			}
			return num;
		}

		private static int CalculateSize<T>(IList<T> value)
		{
			if (value == null)
			{
				return 1;
			}
			int num = SerializedSizeCalculator.CalculateSizeCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				num += SerializedSizeCalculatorHelper<T, SerializedSizeCalculator>.CalculateSerializedSize(value[i]);
				i++;
			}
			return num;
		}

		private static readonly Guid arrayGuid = typeof(Array).GUID;
	}
}
