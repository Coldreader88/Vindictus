using System;
using System.Text;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingRoomInfo
	{
		public long HousingID { get; set; }

		public string Description { get; set; }

		public int OpenLevel { get; set; }

		public HousingRoomInfo(long housingID, string description, int openLevel)
		{
			this.HousingID = housingID;
			this.Description = description;
			this.OpenLevel = openLevel;
		}

		public override string ToString()
		{
			new StringBuilder("HousingRoomInfo");
			return string.Format("[ housingId = {0} / Desc =  {1} / OpenLevel = {2}]", this.HousingID, this.Description, this.OpenLevel);
		}
	}
}
