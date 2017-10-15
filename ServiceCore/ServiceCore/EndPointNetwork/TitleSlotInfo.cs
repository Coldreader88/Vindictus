using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TitleSlotInfo
	{
		public int TitleID { get; set; }

		public TitleState Status { get; set; }

		public long TimeStamp { get; set; }

		public string QuestID { get; set; }

		public ICollection<KeyValuePair<int, int>> GoalProgress { get; set; }

		public bool IsExpireable { get; set; }

		public long ExpireDateTime { get; set; }

		public TitleSlotInfo()
		{
		}

		public TitleSlotInfo(int titleID, TitleState status, long timestamp, string questID, ICollection<KeyValuePair<int, int>> goalProgress, DateTime? expireDateTime)
		{
			this.TitleID = titleID;
			this.Status = status;
			this.TimeStamp = timestamp;
			this.QuestID = questID;
			this.GoalProgress = goalProgress;
			if (expireDateTime == null)
			{
				this.IsExpireable = false;
				this.ExpireDateTime = -1L;
				return;
			}
			this.IsExpireable = true;
			this.ExpireDateTime = expireDateTime.Value.Ticks;
		}

		public override string ToString()
		{
			return string.Format("{0}( status = {1} goalprogress x {2})", this.TitleID, this.Status, this.GoalProgress.Count);
		}
	}
}
