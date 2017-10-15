using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MarbleDice
	{
		public int DiceID { get; set; }

		public int DiceType { get; set; }

		public string ItemClass { get; set; }

		public MarbleDice(int diceID, int diceType, string itemClass)
		{
			this.DiceID = diceID;
			this.DiceType = diceType;
			this.ItemClass = itemClass;
		}
	}
}
