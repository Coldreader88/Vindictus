using System;
using ServiceCore.MicroPlayServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateBattleInventoryInTownMessage : IMessage
	{
		public BattleInventory BattleInventory { get; set; }

		public override string ToString()
		{
			return string.Format("UpdateBattleInventoryInTown[]", new object[0]);
		}
	}
}
