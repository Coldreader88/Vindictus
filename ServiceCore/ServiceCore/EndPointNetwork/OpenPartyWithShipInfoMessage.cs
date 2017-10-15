using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class OpenPartyWithShipInfoMessage : IMessage
	{
		public string QuestID { get; private set; }

		public string Title { get; private set; }

		public int SwearID { get; private set; }

		public ShipOptionInfo Option { get; private set; }

		public OpenPartyWithShipInfoMessage(string questID, string title, int swearID, ShipOptionInfo option)
		{
			this.QuestID = questID;
			this.Title = title;
			this.SwearID = swearID;
			this.Option = option;
		}

		public override string ToString()
		{
			return string.Format("OpenPartyWithShipInfoMessage [ questID = {0} title = {1} swearID = {2} ]", this.QuestID, this.Title, this.SwearID);
		}
	}
}
