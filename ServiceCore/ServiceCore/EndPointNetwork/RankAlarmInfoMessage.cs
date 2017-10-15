using System;
using System.Collections.Generic;
using ServiceCore.RankServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RankAlarmInfoMessage : IMessage
	{
		public IList<RankAlarmInfo> RankAlarm { get; set; }

		public RankAlarmInfoMessage(IList<RankAlarmInfo> RankAlarm)
		{
			this.RankAlarm = RankAlarm;
		}

		public override string ToString()
		{
			return string.Format("RankAlarmInfoMessage[{0}]", this.RankAlarm.Count);
		}
	}
}
