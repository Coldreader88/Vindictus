using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class AttendanceInfoMessage : IMessage
	{
		public int EventType { get; set; }

		public int CurrentVersion { get; set; }

		public List<AttendanceDayInfo> AttendanceInfo { get; set; }

		public List<AttendanceDayInfo> BonusRewardInfo { get; set; }

		public HeroesString PeriodText { get; set; }

		public AttendanceInfoMessage(int eventType, int currentVersion, List<AttendanceDayInfo> attendanceInfo, List<AttendanceDayInfo> bonusRewardInfo, HeroesString periodText)
		{
			this.EventType = eventType;
			this.CurrentVersion = currentVersion;
			this.AttendanceInfo = attendanceInfo;
			this.BonusRewardInfo = bonusRewardInfo;
			this.PeriodText = periodText;
		}

		public override string ToString()
		{
			return string.Format("AttendanceInfoMessage[ AttendanceDayInfo x {0}, BonusReard x {1} ]", this.AttendanceInfo.Count, this.BonusRewardInfo.Count);
		}
	}
}
