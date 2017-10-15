using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PingMessage : IMessage
	{
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		public PingMessage(int t)
		{
			this.tag = t;
		}

		public override string ToString()
		{
			return string.Format("PingMessage[ tag = {0} ]", this.tag);
		}

		private int tag;
	}
}
