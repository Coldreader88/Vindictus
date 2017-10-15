using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;

namespace Devcat.Core.Net.Message
{
	public class MessageAnalyzer : IPacketAnalyzer, IEnumerable<ArraySegment<byte>>, IEnumerable
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GlobalMemoryStatusEx([In] [Out] MEMORYSTATUSEX lpBuffer);

		public event Action<int, string> OnHugeMessageReceive;

		public event Action<string> WriteLog;

		public ICryptoTransform CryptoTransform { get; set; }

		public void Add(ArraySegment<byte> segment)
		{
			if (this.length != this.position)
			{
				throw new Exception("이전에 수신된 데이터를 모두 소비하지 않은 채로 새 데이터가 추가되었습니다.");
			}
			this.buffer = segment.Array;
			this.length = segment.Offset + segment.Count;
			this.position = segment.Offset;
		}

		public IEnumerator<ArraySegment<byte>> GetEnumerator()
		{
			while (this.HasPacket)
			{
				yield return this.packet.Bytes;
				this.packet = default(Packet);
			}
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private bool HasPacket
		{
			get
			{
				if (this.packet.Array == null)
				{
					if (!this.TokenizeInstanceId())
					{
						return false;
					}
					if (!this.TokenizeCategoryId())
					{
						return false;
					}
					if (!this.TokenizeLength())
					{
						return false;
					}
				}
				return this.TokenizeBody();
			}
		}

		private unsafe bool TokenizeInstanceId()
		{
			if (this.length <= this.position)
			{
				return false;
			}
			int num = 9;
			if (num <= this.headerOffset)
			{
				return true;
			}
			int num2 = System.Math.Min(num - this.headerOffset, this.length - this.position);
			Buffer.BlockCopy(this.buffer, this.position, this.header, this.headerOffset, num2);
			this.headerOffset += num2;
			this.position += num2;
			if (this.headerOffset < num)
			{
				return false;
			}
			if (this.CryptoTransform != null && this.CryptoTransform.Decrypt(new ArraySegment<byte>(this.header, 0, num), IPAddress.NetworkToHostOrder(BitConverter.ToInt64(this.header, 0))))
			{
				fixed (byte* ptr = &this.header[0])
				{
					*(long*)ptr = 0L;
				}
			}
			this.categoryBytesCount = Packet.GetBytesCount(this.header, 8);
			return true;
		}

		private bool TokenizeCategoryId()
		{
			if (this.length <= this.position)
			{
				return false;
			}
			int num = 8 + this.categoryBytesCount + 1;
			if (num <= this.headerOffset)
			{
				return true;
			}
			int num2 = System.Math.Min(num - this.headerOffset, this.length - this.position);
			Buffer.BlockCopy(this.buffer, this.position, this.header, this.headerOffset, num2);
			this.headerOffset += num2;
			this.position += num2;
			if (this.headerOffset < num)
			{
				return false;
			}
			if (this.CryptoTransform != null)
			{
				this.CryptoTransform.Decrypt(new ArraySegment<byte>(this.header, 9, this.categoryBytesCount));
			}
			this.lengthBytesCount = Packet.GetBytesCount(this.header, 8 + this.categoryBytesCount);
			return true;
		}

		private bool TokenizeLength()
		{
			if (this.length <= this.position)
			{
				return false;
			}
			int num = 8 + this.categoryBytesCount + this.lengthBytesCount;
			int num2 = System.Math.Min(num - this.headerOffset, this.length - this.position);
			Buffer.BlockCopy(this.buffer, this.position, this.header, this.headerOffset, num2);
			this.headerOffset += num2;
			this.position += num2;
			if (this.headerOffset < num)
			{
				return false;
			}
			if (this.CryptoTransform != null)
			{
				this.CryptoTransform.Decrypt(new ArraySegment<byte>(this.header, 8 + this.categoryBytesCount + 1, this.lengthBytesCount - 1));
			}
			int @int = Packet.GetInt32(this.header, 8);
			int int2 = Packet.GetInt32(this.header, 8 + this.categoryBytesCount);
			if (int2 >= this.HugeMessageSize && this.OnHugeMessageReceive != null)
			{
				this.OnHugeMessageReceive(int2, "");
			}
			try
			{
				this.packet = new Packet(int2, @int);
			}
			catch (OutOfMemoryException ex)
			{
				string text = string.Format("headerLength {0}, recvCount {1}, categoryID {2}, bodyLength {3}\n", new object[]
				{
					num,
					num2,
					@int,
					int2
				});
				MEMORYSTATUSEX memorystatusex = new MEMORYSTATUSEX();
				if (MessageAnalyzer.GlobalMemoryStatusEx(memorystatusex))
				{
					text += string.Format("dwLength                ={0:N}\n", memorystatusex.dwLength);
					text += string.Format("dwMemoryLoad            ={0:N}\n", memorystatusex.dwMemoryLoad);
					text += string.Format("ullTotalPhys            ={0:N}\n", memorystatusex.ullTotalPhys);
					text += string.Format("ullAvailPhys            ={0:N}\n", memorystatusex.ullAvailPhys);
					text += string.Format("ullTotalPageFile        ={0:N}\n", memorystatusex.ullTotalPageFile);
					text += string.Format("ullAvailPageFile        ={0:N}\n", memorystatusex.ullAvailPageFile);
					text += string.Format("ullTotalVirtual         ={0:N}\n", memorystatusex.ullTotalVirtual);
					text += string.Format("ullAvailVirtual         ={0:N}\n", memorystatusex.ullAvailVirtual);
					text += string.Format("ullAvailExtendedVirtual ={0:N}\n", memorystatusex.ullAvailExtendedVirtual);
				}
				if (this.WriteLog != null)
				{
					this.WriteLog(text);
				}
				throw ex;
			}
			Buffer.BlockCopy(this.header, 0, this.packet.Array, this.packet.Offset, 8);
			this.packetOffset = num;
			this.headerOffset = 0;
			this.categoryBytesCount = 1;
			this.lengthBytesCount = 1;
			return true;
		}

		private bool TokenizeBody()
		{
			int num = System.Math.Min(this.packet.Count - this.packetOffset, this.length - this.position);
			Buffer.BlockCopy(this.buffer, this.position, this.packet.Array, this.packet.Offset + this.packetOffset, num);
			this.packetOffset += num;
			this.position += num;
			if (this.packetOffset < this.packet.Count)
			{
				return false;
			}
			if (this.CryptoTransform != null)
			{
				this.CryptoTransform.Decrypt(new ArraySegment<byte>(this.packet.Array, this.packet.Offset + this.packet.BodyOffset, this.packet.Length));
			}
			return true;
		}

		private byte[] buffer;

		private int length;

		private int position;

		private int HugeMessageSize = 102400;

		private byte[] header = new byte[20];

		private int headerOffset;

		private int categoryBytesCount = 1;

		private int lengthBytesCount = 1;

		private Packet packet;

		private int packetOffset;
	}
}
