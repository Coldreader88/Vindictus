using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RecommendShipRestriction
	{
		public int Difficulty { get; set; }

		public bool IsSeason2 { get; set; }

		public ICollection<int> SelectedBossQuestIDInfos { get; set; }

		public override string ToString()
		{
			return string.Format("Questset {0} / {1}", this.QuestSet, this.TargetQuestID);
		}

		public int QuestSet;

		public string TargetQuestID;
	}
}
