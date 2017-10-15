using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Devcat.Core.Memory;
using Devcat.Core.Net;

namespace Nexon.Nisms
{
	public class PacketAnalyzer : IPacketAnalyzer, IEnumerable<ArraySegment<byte>>, IEnumerable
	{
		public event Action<int, string> OnHugeMessageReceive = delegate { };

		public event Action<string> WriteLog = delegate { };

		public ICryptoTransform CryptoTransform
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

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
				yield return this.data;
				this.data = default(ArraySegment<byte>);
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
				return (this.data.Array != null || this.TokenizeHeader()) && this.TokenizeData();
			}
		}

		private bool TokenizeHeader()
		{
			if (this.length <= this.position)
			{
				return false;
			}
			int num = Math.Min(this.header.Length - this.headerOffset, this.length - this.position);
			Buffer.BlockCopy(this.buffer, this.position, this.header, this.headerOffset, num);
			this.headerOffset += num;
			this.position += num;
			if (this.headerOffset < this.header.Length)
			{
				return false;
			}
			int num2 = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(this.header, 1));
			this.headerOffset = 0;
			this.data = ChunkSegment<byte>.Get(this.header.Length + num2);
			Buffer.BlockCopy(this.header, 0, this.data.Array, this.data.Offset, this.header.Length);
			this.dataOffset = this.header.Length;
			return true;
		}

		private bool TokenizeData()
		{
			int num = Math.Min(this.data.Count - this.dataOffset, this.length - this.position);
			Buffer.BlockCopy(this.buffer, this.position, this.data.Array, this.data.Offset + this.dataOffset, num);
			this.dataOffset += num;
			this.position += num;
			return this.dataOffset == this.data.Count;
		}

		private byte[] buffer;

		private int length;

		private int position;

		private byte[] header = new byte[5];

		private int headerOffset;

		private ArraySegment<byte> data;

		private int dataOffset;
	}
}
