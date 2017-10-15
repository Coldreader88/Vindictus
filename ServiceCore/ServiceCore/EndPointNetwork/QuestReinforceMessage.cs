using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestReinforceMessage : IMessage
	{
		public long ShipID { get; private set; }

		public override string ToString()
		{
			return string.Format("QuestReinforceMessage[ ShipID = {0} ]", this.ShipID);
		}
	}
}
