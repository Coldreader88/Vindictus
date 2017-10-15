using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class AttendanceDayInfo
	{
		public int day { get; set; }

		public bool isCompleted { get; set; }

		public int completedRewardIndex { get; set; }

		public AttendanceDayInfo(int day, int completedRewardIndex)
		{
			this.day = day;
			this.completedRewardIndex = completedRewardIndex;
			this.isCompleted = (completedRewardIndex > 0);
		}
	}
}
