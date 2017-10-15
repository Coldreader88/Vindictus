using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PutErgMessage : IMessage
	{
		public PutErgMessage(int e, int t)
		{
			this.ergID = e;
			this.tag = t;
		}

		public override string ToString()
		{
			return string.Format("PutErgMessage[ ergID = {0} tag = {1} ]", this.ergID, this.tag);
		}

		private int ergID;

		private int tag;
	}
}
