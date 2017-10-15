using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GiveAPMessage : IMessage
	{
		public int AP { get; set; }

		public GiveAPMessage(int ap)
		{
			this.AP = ap;
		}

		public override string ToString()
		{
			return string.Format("GiveAPMessage[ AP = {0} ]", this.AP);
		}
	}
}
