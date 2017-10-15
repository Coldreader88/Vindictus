using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRankOtherCharacterInfoMessage : IMessage
	{
		public long RequesterCID { get; set; }

		public string CharacterName { get; set; }

		public int PeriodType { get; set; }

		public override string ToString()
		{
			return string.Format("QueryRankOtherCharacterInfoMessage {0} {1} {2}", this.RequesterCID, this.CharacterName, this.PeriodType);
		}
	}
}
