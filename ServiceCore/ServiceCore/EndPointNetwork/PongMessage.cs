using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PongMessage : IMessage
	{
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		public PongMessage(int t)
		{
			this.tag = t;
		}

		public override string ToString()
		{
			return string.Format("PongMessage[ tag = {0} ]", this.tag);
		}

		private int tag;
	}
}
