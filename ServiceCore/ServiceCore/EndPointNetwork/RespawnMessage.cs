using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RespawnMessage : IMessage
	{
		public List<string> SpawnerNames { get; set; }

		public int SectorID { get; set; }

		public List<TrialFactorInfo> TrialFactorInfos { get; set; }

		public Dictionary<string, int> MonsterItemDrop { get; set; }

		public RespawnMessage(List<string> spawnerNames, int sectorID, List<TrialFactorInfo> trialFactorInfos, Dictionary<string, int> monsterItemDrop)
		{
			this.SpawnerNames = spawnerNames;
			this.SectorID = sectorID;
			this.TrialFactorInfos = trialFactorInfos;
			this.MonsterItemDrop = monsterItemDrop;
		}

		public override string ToString()
		{
			return string.Format("RespawnMessage [ Spawner x {0} SectorID {1} ]", this.SpawnerNames.Count, this.SectorID);
		}
	}
}
