using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestSectorInfos
	{
		public int CurrentGroup { get; set; }

		public int StartingGroup { get; set; }

		public List<QuestSectorInfo> SectorInfos { get; private set; }

		public int TreasureBoxMaxCount { get; set; }

		public List<int> AltarStatus { get; set; }

		public List<string> BossKilledList { get; set; }

		public QuestSectorInfos()
		{
			this.CurrentGroup = -1;
			this.StartingGroup = 1;
			this.SectorInfos = new List<QuestSectorInfo>();
			this.AltarStatus = new List<int>();
			this.BossKilledList = new List<string>();
		}
	}
}
