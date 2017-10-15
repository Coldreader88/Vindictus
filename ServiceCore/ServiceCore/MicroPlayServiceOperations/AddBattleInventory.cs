using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class AddBattleInventory : Operation
	{
		public long HostCID { get; set; }

		public int Owner { get; set; }

		public int SlotNum { get; set; }

		public bool IsFree { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
