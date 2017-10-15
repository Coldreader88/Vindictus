using System;
using System.Net;
using System.Text;
using Devcat.Core.Memory;
using Devcat.Core.Net;

namespace Nexon.CafeAuth
{
	internal struct Packet : IPacket
	{
		public PacketType PacketType
		{
			get
			{
				return (PacketType)Buffer.GetByte(this.Bytes.Array, this.Bytes.Offset + 3);
			}
			private set
			{
				Buffer.SetByte(this.Bytes.Array, this.Bytes.Offset + 3, (byte)value);
			}
		}

		public ArraySegment<byte> Bytes { get; private set; }

		public Packet(ArraySegment<byte> bytes)
		{
			this = default(Packet);
			this.Bytes = bytes;
			this.offset = this.Bytes.Offset + 4;
		}

		internal Packet(int contentLength, PacketType packetType)
		{
			this = default(Packet);
			if (65534 < contentLength)
			{
				throw new ArgumentOutOfRangeException("contentLength", "65534을 초과합니다.");
			}
			short num = (short)(contentLength + 1);
			this.Bytes = ChunkSegment<byte>.Get((int)(num + 3));
			Buffer.SetByte(this.Bytes.Array, this.Bytes.Offset, 170);
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(num));
			Buffer.BlockCopy(bytes, 0, this.Bytes.Array, this.Bytes.Offset + 1, bytes.Length);
			this.PacketType = packetType;
			this.offset = this.Bytes.Offset + 4;
		}

		public bool EndOfStream
		{
			get
			{
				return this.Bytes.Offset <= this.offset;
			}
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
			byte b = this.ReadByte();
			string @string = Encoding.Default.GetString(this.Bytes.Array, this.offset, (int)b);
			this.offset += (int)b;
			return @string;
		}

		public IPAddress ReadIPAddress()
		{
			IPAddress result = new IPAddress((long)BitConverter.ToInt32(this.Bytes.Array, this.offset));
			this.offset += 4;
			return result;
		}

		public MachineID ReadMachineID()
		{
			byte[] array = new byte[16];
			Buffer.BlockCopy(this.Bytes.Array, this.offset, array, 0, array.Length);
			this.offset += array.Length;
			return new MachineID(array);
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
			int bytes = Encoding.Default.GetBytes(value, 0, value.Length, this.Bytes.Array, this.offset + 1);
			this.Write((byte)bytes);
			this.offset += bytes;
		}

		public void Write(IPAddress value)
		{
			if (value == null)
			{
				value = IPAddress.None;
			}
			byte[] addressBytes = value.GetAddressBytes();
			Buffer.BlockCopy(addressBytes, 0, this.Bytes.Array, this.offset, addressBytes.Length);
			this.offset += addressBytes.Length;
		}

		public void Write(MachineID value)
		{
			byte[] array = value.ToByteArray();
			Buffer.BlockCopy(array, 0, this.Bytes.Array, this.offset, array.Length);
			this.offset += array.Length;
		}

		public void Encrypt(ICryptoTransform crypto)
		{
			if (crypto != null)
			{
				throw new ArgumentException("ICryptoTransform is not supported.", "crypto");
			}
		}

		public const byte PacketHeader = 170;

		private int offset;
	}
}
