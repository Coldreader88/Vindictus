using System;
using System.IO;
using System.Text;

namespace PSConsole
{
	internal class SyncTextReader : StreamReader
	{
		protected SyncTextReader() : base(string.Empty)
		{
		}

		public SyncTextReader(Stream stream) : base(stream)
		{
		}

		public SyncTextReader(Stream stream, Encoding encoding) : base(stream, encoding)
		{
		}

		public override int Read()
		{
			return this.BaseStream.ReadByte();
		}

		public override int Read(char[] buffer, int index, int count)
		{
			byte[] array = new byte[count];
			int num = this.BaseStream.Read(array, 0, count);
			if (num > 0)
			{
				Array.Copy(array, 0, buffer, index, num);
			}
			return num;
		}

		public override int Peek()
		{
			if (this.BaseStream.CanSeek)
			{
				int num = this.Read();
				if (num != -1)
				{
					this.BaseStream.Seek(-1L, SeekOrigin.Current);
				}
				return num;
			}
			return -1;
		}

		public override string ReadLine()
		{
			return base.ReadLine();
		}
	}
}
