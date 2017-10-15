using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRankFavoritesInfoMessage : IMessage
	{
		public long CID { get; set; }

		public int PeriodType { get; set; }

		public override string ToString()
		{
			return string.Format("QueryRankFavoritesInfoMessage {0} {1}", this.CID, this.PeriodType);
		}
	}
}
