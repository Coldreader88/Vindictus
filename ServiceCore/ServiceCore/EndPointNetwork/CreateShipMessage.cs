using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CreateShipMessage : IMessage
	{
		public string QuestName
		{
			get
			{
				return this.questName;
			}
		}

		public CreateShipMessage(string quest)
		{
			this.questName = quest;
		}

		public override string ToString()
		{
			return string.Format("CreateShipMessage[ questName = {0} ]", this.questName);
		}

		private string questName;
	}
}
