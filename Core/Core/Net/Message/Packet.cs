using System;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Devcat.Core.Memory;

namespace Devcat.Core.Net.Message
{
	[Serializable]
	public struct Packet : IPacket
	{
		public ArraySegment<byte> Bytes { get; private set; }

		internal byte[] Array
		{
			get
			{
				return this.Bytes.Array;
			}
		}

		internal int Offset
		{
			get
			{
				return this.Bytes.Offset;
			}
		}

		internal int Count
		{
			get
			{
				return this.Bytes.Count;
			}
		}

        public unsafe long InstanceId
        {
            get
            {
                fixed (byte* numRef = &(this.Array[this.Offset]))
                {
                    return IPAddress.NetworkToHostOrder(*((long*)numRef));
                }
            }
            set
            {
                fixed (byte* numRef = &(this.Array[this.Offset]))
                {
                    *((long*)numRef) = IPAddress.HostToNetworkOrder(value);
                }
            }
        }

        public int CategoryId
		{
			get
			{
				return Packet.GetInt32(this.Array, this.Offset + 8);
			}
			private set
			{
				Packet.GetBytes(value, this.Array, this.Offset + 8);
			}
		}

		private int LengthOffset
		{
			get
			{
				return 8 + Packet.GetBytesCount(this.Array, this.Offset + 8);
			}
		}

		public int Length
		{
			get
			{
				return Packet.GetInt32(this.Array, this.Offset + this.LengthOffset);
			}
			private set
			{
				Packet.GetBytes(value, this.Array, this.Offset + this.LengthOffset);
			}
		}

		public int BodyOffset
		{
			get
			{
				int lengthOffset = this.LengthOffset;
				return lengthOffset + Packet.GetBytesCount(this.Array, this.Offset + lengthOffset);
			}
		}

		internal Packet(int length)
		{
			this = new Packet(length, 0);
		}

		internal Packet(int length, int categoryId)
		{
			this = new Packet(ChunkSegment<byte>.Get(8 + Packet.GetBytesCount(categoryId) + Packet.GetBytesCount(length) + length));
			this.CategoryId = categoryId;
			this.Length = length;
		}

		public Packet(ArraySegment<byte> bytes)
		{
			this = default(Packet);
			this.Bytes = bytes;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Packet { InstanceId = ");
			stringBuilder.AppendFormat("0x{0:X16}", this.InstanceId);
			stringBuilder.Append(", CategoryId = ");
			stringBuilder.AppendFormat("0x{0:X8}", this.CategoryId);
			stringBuilder.Append(", Length = ");
			stringBuilder.Append(this.Length);
			stringBuilder.Append(", Body = ");
			int bodyOffset = this.BodyOffset;
			stringBuilder.Append(BitConverter.ToString(this.Bytes.Array, this.Bytes.Offset + bodyOffset, System.Math.Min(this.Count - bodyOffset, 16)));
			if (16 < this.Count)
			{
				stringBuilder.Append("...");
			}
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		public override int GetHashCode()
		{
			return this.Bytes.Array.GetHashCode() ^ this.Bytes.Offset ^ this.Bytes.Count;
		}

		public override bool Equals(object obj)
		{
			return obj is Packet && this.Equals((Packet)obj);
		}

		public bool Equals(Packet obj)
		{
			return obj.Bytes == this.Bytes;
		}

		public static bool operator ==(Packet packet1, Packet packet2)
		{
			return packet1.Equals(packet2);
		}

		public static bool operator !=(Packet packet1, Packet packet2)
		{
			return !(packet1 == packet2);
		}

		internal static int GetBytesCount(int value)
		{
			if (value <= 65535)
			{
				if (value <= 127)
				{
					return 1;
				}
				if (value > 2047)
				{
					return 3;
				}
				return 2;
			}
			else
			{
				if (value <= 2097151)
				{
					return 4;
				}
				if (value > 67108863)
				{
					return 6;
				}
				return 5;
			}
		}

		internal static int GetBytesCount(byte[] bytes, int index)
		{
			byte b = bytes[index];
			if ((b & 128) == 0)
			{
				return 1;
			}
			if ((b & 224) == 192)
			{
				return 2;
			}
			if ((b & 240) == 224)
			{
				return 3;
			}
			if ((b & 248) == 240)
			{
				return 4;
			}
			if ((b & 252) == 248)
			{
				return 5;
			}
			if ((b & 252) == 252)
			{
				return 6;
			}
			throw new SerializationException(string.Format("Invalid leading 6-bits '{0:X2}'", (int)(b & 252)));
		}

		internal static int GetBytes(int value, byte[] bytes, int index)
		{
			int num;
			if (value <= 127)
			{
				num = index + 1;
				bytes[index] = (byte)value;
			}
			else
			{
				if (value <= 2047)
				{
					num = index + 1;
					bytes[index] = (byte)(value >> 6 | 192);
				}
				else
				{
					if (value <= 65535)
					{
						num = index + 1;
						bytes[index] = (byte)(value >> 12 | 224);
					}
					else
					{
						if (value <= 2097151)
						{
							num = index + 1;
							bytes[index] = (byte)(value >> 18 | 240);
						}
						else
						{
							if (value <= 67108863)
							{
								num = index + 1;
								bytes[index] = (byte)(value >> 24 | 248);
							}
							else
							{
								num = index + 1;
								bytes[index] = (byte)((uint)value >> 30 | 252u);
								bytes[num++] = (byte)((value >> 24 & 63) | 128);
							}
							bytes[num++] = (byte)((value >> 18 & 63) | 128);
						}
						bytes[num++] = (byte)((value >> 12 & 63) | 128);
					}
					bytes[num++] = (byte)((value >> 6 & 63) | 128);
				}
				bytes[num++] = (byte)((value & 63) | 128);
			}
			return num - index;
		}

		internal static int GetInt32(byte[] bytes, int index)
		{
			byte b = bytes[index++];
			int num;
			if ((b & 128) == 0)
			{
				num = (int)b;
			}
			else
			{
				if ((b & 224) == 192)
				{
					num = ((int)b << 6 & 1984);
				}
				else
				{
					if ((b & 240) == 224)
					{
						num = ((int)b << 12 & 61440);
					}
					else
					{
						if ((b & 248) == 240)
						{
							num = ((int)b << 18 & 1835008);
						}
						else
						{
							if ((b & 252) == 248)
							{
								num = ((int)b << 24 & 50331648);
							}
							else
							{
								if ((b & 252) != 252)
								{
									throw new SerializationException(string.Format("Invalid leading 6-bits '{0:X2}'", (int)(b & 252)));
								}
								num = ((int)b << 30 & -1073741824);
								b = bytes[index++];
								if ((b & 192) != 128)
								{
									throw new SerializationException("0x10wwwwww");
								}
								num |= ((int)b << 24 & 1056964608);
								if (num <= 67108863)
								{
									throw new SerializationException("0x111111ww");
								}
							}
							b = bytes[index++];
							if ((b & 192) != 128)
							{
								throw new SerializationException("0x10zzzzzz");
							}
							num |= ((int)b << 18 & 16515072);
							if (num <= 2097151)
							{
								throw new SerializationException("0x111110ww");
							}
						}
						b = bytes[index++];
						if ((b & 192) != 128)
						{
							throw new SerializationException("0x10zzyyyy");
						}
						num |= ((int)b << 12 & 258048);
						if (num <= 65535)
						{
							throw new SerializationException("0x11110zzz");
						}
					}
					b = bytes[index++];
					if ((b & 192) != 128)
					{
						throw new SerializationException("0x10yyyyxx");
					}
					num |= ((int)b << 6 & 4032);
					if (num <= 2047)
					{
						throw new SerializationException("0x1110yyyy");
					}
				}
				b = bytes[index++];
				if ((b & 192) != 128)
				{
					throw new SerializationException("0x10xxxxxx");
				}
				num |= (int)(b & 63);
				if (num <= 127)
				{
					throw new SerializationException("0x110yyyxx");
				}
			}
			return num;
		}

		public void Encrypt(ICryptoTransform crypto)
		{
			if (crypto == null)
			{
				throw new ArgumentNullException("crypto");
			}
			long instanceId;
			if (crypto.Encrypt(this.Bytes, out instanceId))
			{
				this.InstanceId = instanceId;
			}
		}
	}
}
