using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestReinforceGrantedMessage : IMessage
	{
		public GameInfo GameInfo { get; set; }

		public int UID { get; set; }

		public int Key { get; set; }

		public override string ToString()
		{
			return string.Format("QuestReinforceGrantedMessage[ GameInfo = {0} UID = {1} Key = {2} ]", this.GameInfo, this.UID, this.Key);
		}
	}
}
