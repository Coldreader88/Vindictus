using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TcProtectRespondMessage : IMessage
	{
		public int Md5Check { get; set; }

		public int ImpressCheck { get; set; }

		public TcProtectRespondMessage(int md5Check, int impressCheck)
		{
			this.Md5Check = md5Check;
			this.ImpressCheck = impressCheck;
		}
	}
}
