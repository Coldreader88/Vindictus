using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class OpenTreasureBoxMessage : IMessage
	{
		public int GroupID { get; set; }

		public string TreasureBoxName { get; set; }

		public override string ToString()
		{
			return string.Format("OpenTreasureBoxMessage[ GroupID = {0} TreasureBoxName = {1} ]", this.GroupID, this.TreasureBoxName);
		}
	}
}
