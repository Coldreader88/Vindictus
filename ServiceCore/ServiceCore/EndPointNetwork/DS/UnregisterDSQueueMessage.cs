using System;

namespace ServiceCore.EndPointNetwork.DS
{
	[Serializable]
	public sealed class UnregisterDSQueueMessage : IMessage
	{
		public string QuestID { get; set; }

		public override string ToString()
		{
			return string.Format("UnregisterDSQueueMessage[ {0} ]", this.QuestID);
		}
	}
}
