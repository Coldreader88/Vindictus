using System;
using System.Net;
using Devcat.Core.Memory;
using Devcat.Core.Net;

namespace Nexon.Nisms
{
	internal struct Packet : IPacket
	{
		public int PacketNo
		{
			get
			{
				return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(this.Bytes.Array, this.Bytes.Offset + 5));
			}
			private set
			{
				Buffer.BlockCopy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value)), 0, this.Bytes.Array, this.Bytes.Offset + 5, 4);
			}
		}

		public PacketType PacketType
		{
			get
			{
				return (PacketType)Buffer.GetByte(this.Bytes.Array, this.Bytes.Offset + 9);
			}
			private set
			{
				Buffer.SetByte(this.Bytes.Array, this.Bytes.Offset + 9, (byte)value);
			}
		}

		public ArraySegment<byte> Bytes { get; private set; }

		public Packet(ArraySegment<byte> bytes)
		{
			this = default(Packet);
			this.Bytes = bytes;
			this.offset = this.Bytes.Offset + 10;
		}

		public Packet(int length, PacketType packetType)
		{
			this = default(Packet);
			this.Bytes = ChunkSegment<byte>.Get(10 + length);
			Buffer.SetByte(this.Bytes.Array, this.Bytes.Offset, 175);
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(length + 5));
			Buffer.BlockCopy(bytes, 0, this.Bytes.Array, this.Bytes.Offset + 1, bytes.Length);
			this.PacketNo = ++Packet.packetNo;
			this.PacketType = packetType;
			this.offset = this.Bytes.Offset + 10;
		}

		public bool ReadBoolean()
		{
			return BitConverter.ToBoolean(this.Bytes.Array, this.offset++);
		}

		public byte ReadByte()
		{
			return Buffer.GetByte(this.Bytes.Array, this.offset++);
		}

		public short ReadInt16()
		{
			short result = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(this.Bytes.Array, this.offset));
			this.offset += 2;
			return result;
		}

		public int ReadInt32()
		{
			int result = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(this.Bytes.Array, this.offset));
			this.offset += 4;
			return result;
		}

		public long ReadInt64()
		{
			long result = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(this.Bytes.Array, this.offset));
			this.offset += 8;
			return result;
		}

		public string ReadString()
		{
			ushort num = (ushort)this.ReadInt16();
			string @string = Connection.EncodeType.GetString(this.Bytes.Array, this.offset, (int)num);
			this.offset += (int)num;
			return @string;
		}

		public DateTime ReadDateTime()
		{
			long ticks = this.ReadInt64();
			DateTime result = new DateTime(ticks);
			return result;
		}

		public Guid ReadGuid()
		{
			string g = this.ReadString();
			return new Guid(g);
		}

		public IPAddress ReadIPAddress()
		{
			IPAddress result = new IPAddress((long)BitConverter.ToInt32(this.Bytes.Array, this.offset));
			this.offset += 4;
			return result;
		}

		public void Write(bool value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, this.Bytes.Array, this.offset, bytes.Length);
			this.offset += bytes.Length;
		}

		public void Write(byte value)
		{
			Buffer.SetByte(this.Bytes.Array, this.offset++, value);
		}

		public void Write(short value)
		{
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
			Buffer.BlockCopy(bytes, 0, this.Bytes.Array, this.offset, bytes.Length);
			this.offset += bytes.Length;
		}

		public void Write(int value)
		{
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
			Buffer.BlockCopy(bytes, 0, this.Bytes.Array, this.offset, bytes.Length);
			this.offset += bytes.Length;
		}

		public void Write(long value)
		{
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
			Buffer.BlockCopy(bytes, 0, this.Bytes.Array, this.offset, bytes.Length);
			this.offset += bytes.Length;
		}

		public void Write(string value)
		{
			if (value == null)
			{
				this.Write(0);
				return;
			}
			int bytes = Connection.EncodeType.GetBytes(value, 0, value.Length, this.Bytes.Array, this.offset + 2);
			this.Write((short)((ushort)bytes));
			this.offset += bytes;
		}

		public void Write(DateTime value)
		{
			this.Write(value.Ticks);
		}

		public void Write(Guid value)
		{
			this.Write(value.ToString());
		}

		public void Write(IPAddress value)
		{
			byte[] addressBytes = value.GetAddressBytes();
			Buffer.BlockCopy(addressBytes, 0, this.Bytes.Array, this.offset, 4);
			this.offset += 4;
		}

		public void Encrypt(ICryptoTransform crypto)
		{
			if (crypto != null)
			{
				throw new ArgumentException("ICryptoTransform is not supported.", "crypto");
			}
		}

		public const byte ReservedChar = 175;

		[ThreadStatic]
		private static int packetNo;

		private int offset;
	}
}
