using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestProgressInfo
	{
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		public int Progress
		{
			get
			{
				return this.progress;
			}
		}

		public int ClearCount
		{
			get
			{
				return this.clearCount;
			}
		}

		public bool TodayQuest
		{
			get
			{
				return this.todayQuest;
			}
		}

		public bool AppearNamedMonster
		{
			get
			{
				return this.appearNamedMonster;
			}
		}

		public bool Available
		{
			get
			{
				return this.available;
			}
		}

		public bool Revealed
		{
			get
			{
				return this.revealed;
			}
		}

		public int MaxPlayCount
		{
			get
			{
				return this.maxPlayCount;
			}
		}

		public int PlayCount
		{
			get
			{
				return this.playCount;
			}
		}

		public List<ClearTimeElement> ClearTimes
		{
			get
			{
				return this.clearTimes;
			}
		}

		public QuestProgressInfo(string id, int progress, int clearCount, bool todayQuest, bool appearNamedMonster, bool available, bool revealed, int maxPlayCount, int todayPlayCount, IEnumerable<ClearTimeElement> clearTimes)
		{
			this.id = id;
			this.progress = progress;
			this.clearCount = clearCount;
			this.todayQuest = todayQuest;
			this.appearNamedMonster = appearNamedMonster;
			this.available = available;
			this.revealed = revealed;
			this.maxPlayCount = maxPlayCount;
			this.playCount = todayPlayCount;
			if (clearTimes != null)
			{
				this.clearTimes = clearTimes.ToList<ClearTimeElement>();
			}
		}

		public override string ToString()
		{
			return string.Format("{0}({1}, {2}, {3}, {4})", new object[]
			{
				this.id,
				this.progress,
				this.clearCount,
				this.todayQuest,
				this.appearNamedMonster
			});
		}

		private string id;

		private int progress;

		private int clearCount;

		private bool todayQuest;

		private bool appearNamedMonster;

		private bool available;

		private bool revealed;

		private int maxPlayCount;

		private int playCount;

		private List<ClearTimeElement> clearTimes;
	}
}
