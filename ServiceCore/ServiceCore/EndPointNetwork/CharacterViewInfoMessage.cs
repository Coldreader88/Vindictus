using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CharacterViewInfoMessage : IMessage
	{
		public long QueryID { get; set; }

		public CharacterSummary Summary { get; private set; }

		public IDictionary<string, int> Stat { get; private set; }

		public QuickSlotInfo QuickSlotInfo { get; private set; }

		public IDictionary<int, ColoredEquipment> Equipment { get; private set; }

		public int SilverCoin { get; set; }

		public int PlatinumCoin { get; set; }

		public IDictionary<int, DurabilityEquipment> Durability { get; private set; }

		public CharacterViewInfoMessage(long id, CharacterSummary summary, IDictionary<string, int> stat, QuickSlotInfo qslot, IDictionary<int, ColoredEquipment> equip, int silverCoin, int platinumCoin, IDictionary<int, DurabilityEquipment> durability)
		{
			this.QueryID = id;
			this.Summary = summary;
			this.Stat = stat;
			this.QuickSlotInfo = qslot;
			this.Equipment = equip;
			this.SilverCoin = silverCoin;
			this.PlatinumCoin = platinumCoin;
			this.Durability = durability;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("CharacterViewInfoMessage [ ");
			stringBuilder.Append(" ]");
			return stringBuilder.ToString();
		}
	}
}
