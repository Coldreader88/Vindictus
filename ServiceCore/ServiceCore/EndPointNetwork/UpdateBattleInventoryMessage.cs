using System;
using ServiceCore.MicroPlayServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateBattleInventoryMessage : IMessage
	{
		public int Tag { get; set; }

		public int UID { get; set; }

		public BattleInventory BattleInventory { get; set; }

		public override string ToString()
		{
			return string.Format("UpdateBattleInventory[]", new object[0]);
		}
	}
}
