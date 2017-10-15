using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BuildingInfo
	{
		public string BuildingID { get; set; }

		public ICollection<NpcInfo> Npcs { get; set; }

		public BuildingInfo(string buildingID, ICollection<NpcInfo> npcs)
		{
			this.BuildingID = buildingID;
			this.Npcs = npcs;
		}

		public override string ToString()
		{
			return this.BuildingID;
		}
	}
}
