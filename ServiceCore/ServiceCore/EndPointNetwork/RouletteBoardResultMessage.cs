using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RouletteBoardResultMessage : IMessage
	{
		public RouletteBoardResultMessage.RouletteBoardResult Result { get; set; }

		public long CID { get; set; }

		public int RemindsSeconds { get; set; }

		public List<RouletteSlotInfo> SlotInfos { get; set; }

		public override string ToString()
		{
			return string.Format("RouletteBoardResultMessage - Result [{0}] CID [{1}] RemindsSeconds [{2}] Count [{3}]", new object[]
			{
				this.Result,
				this.CID,
				this.RemindsSeconds,
				this.SlotInfos.Count
			});
		}

		public enum RouletteBoardResult
		{
			Result_OK,
			Result_FeatureOff
		}
	}
}
