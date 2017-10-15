using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestSectorInfo
	{
		public int GroupID { get; set; }

		public string Bsp { get; set; }

		public List<string> Entities { get; set; }

		public List<QuestSectorConnectionInfo> ConnectionInfos { get; private set; }

		public Dictionary<string, int> ItemDropEntity { get; set; }

		public Dictionary<string, int> WeakPointEntity { get; set; }

		public List<TrialFactorInfo> TrialFactorInfos { get; set; }

		public List<TreasureBoxInfo> TreasureBoxInfos { get; set; }

		public bool SaveRequired { get; set; }

		public string LastUsedSpawnPoint { get; set; }

		public QuestSectorInfo(int groupID, string bsp, List<string> entities, IEnumerable<QuestSectorConnectionInfo> connections, bool saveRequired, string lastUsedSpawnPoint)
		{
			this.GroupID = groupID;
			this.Bsp = bsp;
			this.Entities = entities;
			this.ConnectionInfos = connections.ToList<QuestSectorConnectionInfo>();
			this.ItemDropEntity = new Dictionary<string, int>();
			this.WeakPointEntity = new Dictionary<string, int>();
			this.TrialFactorInfos = new List<TrialFactorInfo>();
			this.TreasureBoxInfos = new List<TreasureBoxInfo>();
			this.SaveRequired = saveRequired;
			this.LastUsedSpawnPoint = lastUsedSpawnPoint;
		}
	}
}
