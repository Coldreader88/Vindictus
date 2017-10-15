using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestTimerInfoMessage : IMessage
	{
		public int QuestTime { get; set; }

		public bool IsTimerDecreasing { get; set; }

		public override string ToString()
		{
			return string.Format("QuestTimerInfoMessage[ QuestTime = {0}, IsTimerDecreasing = {1} ]", this.QuestTime, this.IsTimerDecreasing);
		}
	}
}
