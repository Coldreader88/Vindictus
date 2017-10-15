using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Devcat.Core.Net.Message
{
	public struct SerializeWriter
	{
		private SerializeWriter(int length, int categoryId)
		{
			this.data = new Packet(length, categoryId);
			this.position = this.data.Offset + this.data.BodyOffset;
		}

		public static Packet ToBinary<T>(T value)
		{
			int categoryId = ClassInfo<T>.CategoryId;
			if (categoryId == 0)
			{
				return SerializeWriter.ToBinary(value);
			}
			int length = SerializedSizeCalculator.Calculate<T>(value);
			SerializeWriter serializeWriter = new SerializeWriter(length, categoryId);
			SerializeWriterHelper<T, SerializeWriter>.Serialize(ref serializeWriter, value);
			return serializeWriter.data;
		}

		public static Packet ToBinary(object value)
		{
			int length = SerializedSizeCalculator.Calculate<object>(value);
			SerializeWriter serializeWriter = new SerializeWriter(length, 0);
			SerializeWriterHelper<object, SerializeWriter>.Serialize(ref serializeWriter, value);
			return serializeWriter.data;
		}

		private void Write(sbyte value)
		{
			this.data.Array[this.position++] = (byte)value;
		}

		private void Write(byte value)
		{
			this.data.Array[this.position++] = value;
		}

		private unsafe void Write(short value)
		{
			fixed (byte* ptr = &this.data.Array[this.position])
			{
				*(short*)ptr = IPAddress.HostToNetworkOrder(value);
			}
			this.position += 2;
		}

		private unsafe void Write(ushort value)
		{
			fixed (byte* ptr = &this.data.Array[this.position])
			{
				*(short*)ptr = IPAddress.HostToNetworkOrder((short)value);
			}
			this.position += 2;
		}

		private unsafe void Write(int value)
		{
			fixed (byte* ptr = &this.data.Array[this.position])
			{
				*(int*)ptr = IPAddress.HostToNetworkOrder(value);
			}
			this.position += 4;
		}

		private unsafe void Write(uint value)
		{
			fixed (byte* ptr = &this.data.Array[this.position])
			{
				*(int*)ptr = IPAddress.HostToNetworkOrder((int)value);
			}
			this.position += 4;
		}

		private unsafe void Write(long value)
		{
			fixed (byte* ptr = &this.data.Array[this.position])
			{
				*(long*)ptr = IPAddress.HostToNetworkOrder(value);
			}
			this.position += 8;
		}

		private unsafe void Write(ulong value)
		{
			fixed (byte* ptr = &this.data.Array[this.position])
			{
				*(long*)ptr = IPAddress.HostToNetworkOrder((long)value);
			}
			this.position += 8;
		}

		private unsafe void Write(char value)
		{
			fixed (byte* ptr = &this.data.Array[this.position])
			{
				*(short*)ptr = IPAddress.HostToNetworkOrder((short)value);
			}
			this.position += 2;
		}

		private unsafe void Write(float value)
		{
			fixed (byte* ptr = &this.data.Array[this.position])
			{
				*(int*)ptr = IPAddress.HostToNetworkOrder(*(int*)(&value));
			}
			this.position += 4;
		}

		private unsafe void Write(double value)
		{
			fixed (byte* ptr = &this.data.Array[this.position])
			{
				*(long*)ptr = IPAddress.HostToNetworkOrder(*(long*)(&value));
			}
			this.position += 8;
		}

		private void Write(bool value)
		{
            this.data.Array[this.position++] = value ? ((byte)1) : ((byte)0);
        }

		private void WriteCount(int count)
		{
			if (count <= 127)
			{
				this.Write((byte)count);
				return;
			}
			if (count <= 2047)
			{
				this.Write((byte)((uint)count >> 6 | 192u));
			}
			else
			{
				if (count <= 65535)
				{
					this.Write((byte)((uint)count >> 12 | 224u));
				}
				else
				{
					if (count <= 2097151)
					{
						this.Write((byte)((uint)count >> 18 | 240u));
					}
					else
					{
						if (count <= 67108863)
						{
							this.Write((byte)((uint)count >> 24 | 248u));
						}
						else
						{
							this.Write((byte)((uint)count >> 30 | 252u));
							this.Write((byte)(((uint)count >> 24 & 63u) | 128u));
						}
						this.Write((byte)(((uint)count >> 18 & 63u) | 128u));
					}
					this.Write((byte)(((uint)count >> 12 & 63u) | 128u));
				}
				this.Write((byte)(((uint)count >> 6 & 63u) | 128u));
			}
			this.Write((byte)((count & 63) | 128));
		}

		private unsafe void Write(IntPtr value)
		{
			this.Write((long)value.ToPointer());
        }

		private void Write(string value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(Encoding.UTF8.GetByteCount(value) + 1);
			this.position += Encoding.UTF8.GetBytes(value, 0, value.Length, this.data.Array, this.position);
		}

		private void Write(Type value)
		{
			if (value == null)
			{
				SerializeWriterHelper<Guid, SerializeWriter>.Serialize(ref this, Guid.Empty);
				return;
			}
			if (value.IsArray)
			{
				SerializeWriterHelper<Guid, SerializeWriter>.Serialize(ref this, SerializeWriter.arrayGuid);
				this.Write(value.GetElementType());
				this.WriteCount(value.Name.EndsWith("[]") ? 0 : value.GetArrayRank());
				return;
			}
			if (value.IsGenericType)
			{
				SerializeWriterHelper<Guid, SerializeWriter>.Serialize(ref this, value.GetGenericTypeDefinition().GUID);
				Type[] genericArguments = value.GetGenericArguments();
				int i = 0;
				int num = genericArguments.Length;
				while (i < num)
				{
					this.Write(genericArguments[i]);
					i++;
				}
				return;
			}
			SerializeWriterHelper<Guid, SerializeWriter>.Serialize(ref this, value.GUID);
		}

		private void Write(sbyte[] value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Length + 1);
			Buffer.BlockCopy(value, 0, this.data.Array, this.position, value.Length);
			this.position += value.Length;
		}

		private void Write(byte[] value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Length + 1);
			Buffer.BlockCopy(value, 0, this.data.Array, this.position, value.Length);
			this.position += value.Length;
		}

		private void Write(sbyte? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(byte? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(short? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(ushort? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(int? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(uint? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(long? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(ulong? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(char? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(float? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(double? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write(bool? value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value.Value);
			}
		}

		private void Write<T>(T? value) where T : struct
		{
			this.Write(value != null);
			if (value != null)
			{
				SerializeWriterHelper<T, SerializeWriter>.Serialize(ref this, value.Value);
			}
		}

		private void Write(ArraySegment<sbyte> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			Buffer.BlockCopy(value.Array, value.Offset, this.data.Array, this.position, value.Count);
			this.position += value.Count;
		}

		private void Write(ArraySegment<byte> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			Buffer.BlockCopy(value.Array, value.Offset, this.data.Array, this.position, value.Count);
			this.position += value.Count;
		}

		private void Write(ArraySegment<short> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<ushort> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<int> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<uint> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<long> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<ulong> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<char> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<float> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<double> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<bool> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<string> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write(ArraySegment<Type> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				this.Write(value.Array[i]);
				i++;
			}
		}

		private void Write<T>(ArraySegment<T> value)
		{
			if (value.Array == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = value.Offset;
			int num = value.Offset + value.Count;
			while (i < num)
			{
				SerializeWriterHelper<T, SerializeWriter>.Serialize(ref this, value.Array[i]);
				i++;
			}
		}

		private void Write(ICollection values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (object value in values)
			{
				SerializeWriterHelper<object, SerializeWriter>.Serialize(ref this, value);
			}
		}

		private void Write(ICollection<sbyte> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (sbyte value in values)
			{
				this.Write(value);
			}
		}

		private void Write(ICollection<byte> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (byte value in values)
			{
				this.Write(value);
			}
		}

		private void Write(ICollection<short> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (short value in values)
			{
				this.Write(value);
			}
		}

		private void Write(ICollection<ushort> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (ushort value in values)
			{
				this.Write(value);
			}
		}

		private void Write(ICollection<int> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (int value in values)
			{
				this.Write(value);
			}
		}

		private void Write(ICollection<uint> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (uint value in values)
			{
				this.Write(value);
			}
		}

		private void Write(ICollection<long> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (long value in values)
			{
				this.Write(value);
			}
		}

		private void Write(ICollection<ulong> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (ulong value in values)
			{
				this.Write(value);
			}
		}

		private void Write(ICollection<char> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (char value in values)
			{
				this.Write(value);
			}
		}

		private void Write(ICollection<float> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (float num in values)
			{
				float value = num;
				this.Write(value);
			}
		}

		private void Write(ICollection<double> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (double num in values)
			{
				double value = num;
				this.Write(value);
			}
		}

		private void Write(ICollection<bool> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (bool value in values)
			{
				this.Write(value);
			}
		}

		private void Write<T>(ICollection<T> values)
		{
			if (values == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(values.Count + 1);
			foreach (T value in values)
			{
				SerializeWriterHelper<T, SerializeWriter>.Serialize(ref this, value);
			}
		}

		private void Write(IList<sbyte> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<byte> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<short> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<ushort> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<int> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<uint> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<long> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<ulong> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<char> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<float> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<double> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<bool> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<string> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write(IList<Type> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				this.Write(value[i]);
				i++;
			}
		}

		private void Write<T>(IList<T> value)
		{
			if (value == null)
			{
				this.WriteCount(0);
				return;
			}
			this.WriteCount(value.Count + 1);
			int i = 0;
			int count = value.Count;
			while (i < count)
			{
				SerializeWriterHelper<T, SerializeWriter>.Serialize(ref this, value[i]);
				i++;
			}
		}

		private Packet data;

		private int position;

		private static readonly Guid arrayGuid = typeof(Array).GUID;
	}
}
