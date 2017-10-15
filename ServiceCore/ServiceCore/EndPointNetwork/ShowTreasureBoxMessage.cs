using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ShowTreasureBoxMessage : IMessage
	{
		public int GroupID { get; set; }

		public string TreasureBoxName { get; set; }

		public ShowTreasureBoxMessage(int groupID, string treasureBoxName)
		{
			this.GroupID = groupID;
			this.TreasureBoxName = treasureBoxName;
		}

		public override string ToString()
		{
			return string.Format("ShowTreasureBoxMessage[ GroupID = {0} , TreasureBoxName = {1} ]", this.GroupID, this.TreasureBoxName);
		}
	}
}
