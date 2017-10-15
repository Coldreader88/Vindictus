using System;

namespace ServiceCore.EndPointNetwork.DS
{
	[Serializable]
	public sealed class RegisterDSQueueMessage : IMessage
	{
		public string QuestID { get; set; }

		public override string ToString()
		{
			return string.Format("RegisterDSQueueMessage[ {0} ]", this.QuestID);
		}
	}
}
