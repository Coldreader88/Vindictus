using System;

namespace Nexon.Com.Encryption
{
	public class KeyNIvSet
	{
		public KeyNIvSet(byte[] key, byte[] iv)
		{
			this.Key = key;
			this.Iv = iv;
		}

		public byte[] Key { get; set; }

		public byte[] Iv { get; set; }
	}
}
