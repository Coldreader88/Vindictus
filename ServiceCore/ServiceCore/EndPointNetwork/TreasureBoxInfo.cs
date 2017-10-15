using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TreasureBoxInfo
	{
		public int GroupID { get; set; }

		public string EntityName { get; set; }

		public bool IsVisible { get; set; }

		public bool IsOpend { get; set; }

		public TreasureBoxInfo(int groupID, string name, bool visible, bool open)
		{
			this.GroupID = groupID;
			this.EntityName = name;
			this.IsVisible = visible;
			this.IsOpend = open;
		}
	}
}
