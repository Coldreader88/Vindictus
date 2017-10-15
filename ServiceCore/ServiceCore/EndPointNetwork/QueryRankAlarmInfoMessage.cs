using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRankAlarmInfoMessage : IMessage
	{
		public long CID { get; set; }

		public override string ToString()
		{
			return string.Format("QueryRankAlarmInfoMessage {0}", this.CID);
		}
	}
}
