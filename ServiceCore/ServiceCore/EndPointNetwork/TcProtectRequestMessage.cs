using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TcProtectRequestMessage : IMessage
	{
		public byte[] Md5OfDll { get; set; }

		public byte[] EncodedBlock { get; set; }

		public TcProtectRequestMessage(byte[] md5OfDll, byte[] encodedBlock)
		{
			this.Md5OfDll = md5OfDll;
			this.EncodedBlock = encodedBlock;
		}
	}
}
