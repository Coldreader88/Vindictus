using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NexonSNByNameMessage : IMessage
	{
		public long QueryID { get; set; }

		public int UID { get; set; }

		public override string ToString()
		{
			return string.Format("NexonSNByNameMessage[{0}, UID {1}]", this.QueryID, this.UID);
		}
	}
}
