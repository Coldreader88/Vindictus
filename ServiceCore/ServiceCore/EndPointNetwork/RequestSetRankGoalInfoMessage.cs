using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestSetRankGoalInfoMessage : IMessage
	{
		public long CID { get; set; }

		public string RankID { get; set; }

		public override string ToString()
		{
			return string.Format("RequestSetRankGoalInfoMessage {0}", this.RankID);
		}
	}
}
