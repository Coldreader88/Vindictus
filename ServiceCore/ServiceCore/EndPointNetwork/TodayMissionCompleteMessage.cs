using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public class TodayMissionCompleteMessage : IMessage
	{
		public int ID { get; set; }

		public override string ToString()
		{
			return string.Format("TodayMissionCompleteMessage [ {0} ]", this.ID);
		}
	}
}
