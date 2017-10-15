using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class OpenTreasureBoxResultMessage : IMessage
	{
		public int GroupID { get; set; }

		public string EntityName { get; set; }

		public Dictionary<string, int> MonsterList { get; set; }

		public OpenTreasureBoxResultMessage(int groupID, string entityName, Dictionary<string, int> monsterList)
		{
			this.GroupID = groupID;
			this.EntityName = entityName;
			this.MonsterList = monsterList;
		}

		public override string ToString()
		{
			return string.Format("OpenTreasureBoxResultMessage[ GroupID = {0}, EntityName = {1}, MonsterCount = {2} ]", this.GroupID, this.EntityName, this.MonsterList.Count);
		}
	}
}
