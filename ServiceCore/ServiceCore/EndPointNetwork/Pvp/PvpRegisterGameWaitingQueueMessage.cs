using System;

namespace ServiceCore.EndPointNetwork.Pvp
{
	[Serializable]
	public class PvpRegisterGameWaitingQueueMessage : IMessage
	{
		public int GameIndex { get; set; }

		public override string ToString()
		{
			return string.Format("PvpRegisterWaitingQueue[{0}]", this.GameIndex);
		}
	}
}
