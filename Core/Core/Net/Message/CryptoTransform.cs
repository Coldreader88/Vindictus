using System;
using System.Linq;

namespace Devcat.Core.Net.Message
{
	public class CryptoTransform : ICryptoTransform
	{
		public bool Encrypt(ArraySegment<byte> buffer, out long salt)
		{
			bool flag = this.send == null;
			if (!flag)
			{
				salt = 0L;
			}
			else
			{
				salt = (long)((ulong)CryptoTransform.rand.Next());
				this.send = new Isaac((int)salt);
			}
			foreach (int num in Enumerable.Range(buffer.Offset, buffer.Count))
			{
				byte[] array = buffer.Array;
				int num2 = num;
				array[num2] ^= (byte)this.send.Next();
			}
			return flag;
		}

		public void Decrypt(ArraySegment<byte> buffer)
		{
			if (this.recv == null)
			{
				throw new InvalidOperationException("salt is not set");
			}
			foreach (int num in Enumerable.Range(buffer.Offset, buffer.Count))
			{
				byte[] array = buffer.Array;
				int num2 = num;
				array[num2] ^= (byte)this.recv.Next();
			}
		}

		public bool Decrypt(ArraySegment<byte> buffer, long salt)
		{
			bool result = this.recv == null;
			if (this.recv == null)
			{
				this.recv = new Isaac((int)((uint)salt));
			}
			this.Decrypt(buffer);
			return result;
		}

		private static Random rand = new Random();

		private Isaac send;

		private Isaac recv;
	}
}
