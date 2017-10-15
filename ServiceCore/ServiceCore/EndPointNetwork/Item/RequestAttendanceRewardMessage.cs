using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class RequestAttendanceRewardMessage : IMessage
	{
		public int EventType { get; set; }

		public bool IsBonus { get; set; }

		public override string ToString()
		{
			return string.Format("RequestAttendanceRewardMessage[{0}/{1}]", this.EventType, this.IsBonus.ToString());
		}
	}
}
