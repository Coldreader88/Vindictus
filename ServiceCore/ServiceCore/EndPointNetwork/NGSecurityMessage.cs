using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NGSecurityMessage : IMessage
	{
		public byte[] message { get; set; }

		public ulong checkSum { get; set; }

		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < this.message.Length; i++)
			{
				text += string.Format("{0:X2} ", this.message[i]);
			}
			return string.Format("NGSecurityMessage[ {0} ], CheckSum : {1}", text, this.checkSum);
		}
	}
}
