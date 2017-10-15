using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRankAllInfoMessage : IMessage
	{
		public int PeriodType { get; set; }

		public override string ToString()
		{
			return string.Format("QueryRankAllInfoMessage {0}", this.PeriodType);
		}
	}
}
