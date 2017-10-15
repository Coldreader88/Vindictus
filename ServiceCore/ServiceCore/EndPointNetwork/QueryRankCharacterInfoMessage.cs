using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRankCharacterInfoMessage : IMessage
	{
		public long CID { get; set; }

		public int PeriodType { get; set; }

		public override string ToString()
		{
			return string.Format("QueryRankCharacterInfoMessage {0} {1}", this.CID, this.PeriodType);
		}
	}
}
