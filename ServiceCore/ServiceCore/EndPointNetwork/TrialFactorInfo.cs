using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TrialFactorInfo
	{
		public int GroupNumber { get; set; }

		public int TrialMod { get; set; }

		public string TrialName { get; set; }

		public int SectorGroupID { get; set; }

		public TrialFactorInfo(int mod, int groupNumber, string name, int sectorGroupID)
		{
			this.GroupNumber = groupNumber;
			this.TrialMod = mod;
			this.TrialName = name;
			this.SectorGroupID = sectorGroupID;
		}
	}
}
