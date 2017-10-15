using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StartTrialEventMessage : IMessage
	{
		public int SectorGroupID { get; set; }

		public string FactorName { get; set; }

		public int TimeLimit { get; set; }

		public List<int> ActorsIndex { get; set; }

		public override string ToString()
		{
			return string.Format("StartTrialEventMessage [ {0} Event in {1} SectorGroup, TimeLimit {2}, ActorsCount {3} ]", new object[]
			{
				this.FactorName,
				this.SectorGroupID,
				this.TimeLimit,
				this.ActorsIndex.Count
			});
		}
	}
}
