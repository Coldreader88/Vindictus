using System;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class AttendanceRewardItem
	{
		public int AttendanceDay { get; set; }

		public int RewardID { get; set; }

		public int ShareItemType { get; set; }
	}
}
