using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestListMessage : IMessage
	{
		public ICollection<string> QuestList { get; set; }

		public QuestListMessage()
		{
			this.QuestList = new List<string>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("QuestListMessage [ quests = (");
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
