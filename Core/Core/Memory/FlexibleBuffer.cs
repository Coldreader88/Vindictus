using System;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Memory
{
	public class FlexibleBuffer
	{
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		public int Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.Reserve(value - this.position);
				this.position = value;
			}
		}

		public int Capacity
		{
			get
			{
				return this.buffer.Length;
			}
			set
			{
				this.Reserve(value - this.position);
			}
		}

		public FlexibleBuffer() : this(1024)
		{
		}

		public FlexibleBuffer(int minimumBufferSize)
		{
			this.initBufferSize = minimumBufferSize;
			this.buffer = new byte[this.initBufferSize];
			this.position = 0;
			this.trimCount = 0;
		}

		public void Reserve(int count)
		{
			int i = this.buffer.Length;
			if (i < this.position + count)
			{
				while (i < this.position + count)
				{
					i *= 2;
				}
				byte[] dst = new byte[i];
				System.Buffer.BlockCopy(this.buffer, 0, dst, 0, this.position);
				this.buffer = dst;
				this.trimCount = 0;
			}
		}

		public void Push(byte[] value)
		{
			this.Push(value, value.Length);
		}

		public void Push(Array value, int bytesCount)
		{
			if (value != null && bytesCount > 0)
			{
				this.Reserve(bytesCount);
				System.Buffer.BlockCopy(value, 0, this.buffer, this.position, bytesCount);
				this.position += bytesCount;
			}
		}

		public void Push(Packet value)
		{
			if (value.Count > 0)
			{
				this.Reserve(value.Count);
				System.Buffer.BlockCopy(value.Array, value.Offset, this.buffer, this.position, value.Count);
				this.position += value.Count;
			}
		}

		public void Pop(int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (count > this.position)
			{
				throw new ArgumentOutOfRangeException("count", count, "Too large value");
			}
			if (this.buffer.Length >= this.position * 2 && this.buffer.Length > this.initBufferSize)
			{
				this.trimCount++;
			}
			else
			{
				this.trimCount = 0;
			}
			if (this.trimCount == 3)
			{
				this.trimCount = 0;
				byte[] dst = new byte[this.buffer.Length / 2];
				System.Buffer.BlockCopy(this.buffer, count, dst, 0, this.position - count);
				this.buffer = dst;
			}
			else
			{
				System.Buffer.BlockCopy(this.buffer, count, this.buffer, 0, this.position - count);
			}
			this.position -= count;
		}

		private int initBufferSize;

		private byte[] buffer;

		private int position;

		private int trimCount;
	}
}
