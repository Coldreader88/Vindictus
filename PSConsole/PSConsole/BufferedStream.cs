using System;
using System.IO;
using System.Threading;

namespace PSConsole
{
	internal class BufferedStream : Stream
	{
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		public override long Length
		{
			get
			{
				long result;
				lock (this)
				{
					result = (long)this.currentLength;
				}
				return result;
			}
		}

		public override long Position
		{
			get
			{
				return this.currentPosition;
			}
			set
			{
				if (value == this.currentPosition)
				{
					return;
				}
				if (value <= this.currentPosition)
				{
					if (value < this.currentPosition)
					{
						if (this.currentPosition - value > (long)this.backupSize)
						{
							throw new ArgumentOutOfRangeException("Position");
						}
						this.backupSize -= (int)(this.currentPosition - value);
						this.currentPosition = value;
					}
					return;
				}
				if (value > (long)this.currentLength)
				{
					throw new ArgumentOutOfRangeException("Position");
				}
				this.currentPosition = value;
			}
		}

		public BufferedStream(Stream stream) : this(stream, 4096)
		{
		}

		public BufferedStream(Stream stream, int bufferSize)
		{
			this.internalStream = stream;
			this.backupSize = bufferSize;
			this.bufferSize = bufferSize;
			this.buffer = new byte[bufferSize * 2];
			Thread thread = new Thread(new ThreadStart(this.ReaderThread));
			thread.Start();
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void Flush()
		{
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteByte(byte value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				throw new Exception("The method or operation is not implemented.");
			case SeekOrigin.Current:
				this.Position += offset;
				break;
			case SeekOrigin.End:
				throw new Exception("The method or operation is not implemented.");
			}
			return this.Position;
		}

		public override int Read(byte[] _buffer, int offset, int count)
		{
			int result;
			lock (this)
			{
				int num;
				if ((long)this.currentLength - this.currentPosition > (long)count)
				{
					num = count;
				}
				else
				{
					num = (int)((long)this.currentLength - this.currentPosition);
				}
				if (num != 0)
				{
					Array.Copy(this.buffer, this.currentPosition % (long)(this.bufferSize * 2), _buffer, (long)offset, (long)num);
					this.currentPosition += (long)num;
					if (this.backupSize != this.bufferSize)
					{
						this.backupSize = System.Math.Min(this.bufferSize, this.backupSize + num);
					}
				}
				result = num;
			}
			return result;
		}

		public override int ReadByte()
		{
			int result;
			lock (this)
			{
				if ((long)this.currentLength > this.currentPosition)
				{
					byte[] m_readbyte = this.buffer;
					long num;
					this.currentPosition = (num = this.currentPosition) + 1L;
					result = m_readbyte[(int)(checked((IntPtr)(num % unchecked((long)(this.bufferSize * 2)))))];
				}
				else
				{
					result = -1;
				}
			}
			return result;
		}

		protected void ReaderThread()
		{
			byte[] sourceArray = new byte[1024];
			while (true)
			{
				int num = this.internalStream.Read(sourceArray, 0, 1024);
				if (num != -1)
				{
					lock (this)
					{
						if ((long)(this.currentLength + num) - this.currentPosition >= (long)(this.bufferSize * 2))
						{
							throw new Exception("Not enough buffer");
						}
						Array.Copy(sourceArray, 0, this.buffer, this.currentLength % (this.bufferSize * 2), num);
						this.currentLength += num;
					}
				}
			}
		}

		private const int defaultBufferSize = 4096;

		protected Stream internalStream;

		protected byte[] buffer;

		protected int backupSize;

		protected int bufferSize;

		protected int currentLength;

		protected long currentPosition;
	}
}
