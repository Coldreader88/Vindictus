using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class PickGuildStorageItem : Operation
	{
		public long OwnerCID { get; set; }

		public long ItemID { get; set; }

		public string ItemClass { get; set; }

		public int Amount { get; set; }

		public byte TargetTab { get; set; }

		public int TargetSlot { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
