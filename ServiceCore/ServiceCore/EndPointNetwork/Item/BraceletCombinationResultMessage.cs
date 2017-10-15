using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class BraceletCombinationResultMessage : IMessage
	{
		public long BraceletItemID { get; set; }

		public long GemstoneItemID { get; set; }

		public int ResultCode { get; set; }

		public string ResultMessage { get; set; }

		public Dictionary<string, int> PreviousStat { get; set; }

		public Dictionary<string, int> newStat { get; set; }

		public BraceletCombinationResultMessage(long braceletItemID, long gemstoneItemID, int resultCode, string resultMessage, Dictionary<string, int> previousStat, Dictionary<string, int> newStat)
		{
			this.BraceletItemID = braceletItemID;
			this.GemstoneItemID = gemstoneItemID;
			this.ResultCode = resultCode;
			this.ResultMessage = resultMessage;
			this.PreviousStat = previousStat;
			this.newStat = newStat;
		}

		public override string ToString()
		{
			return string.Format("BraceletCombinationResultMessage {0}/{1}/{2}/{3}", new object[]
			{
				this.BraceletItemID,
				this.GemstoneItemID,
				this.ResultCode,
				this.ResultMessage
			});
		}
	}
}
