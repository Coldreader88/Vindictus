using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class EnhanceItemByData : Operation
	{
		public long ItemID { get; set; }

		public string MaterialSlot1 { get; set; }

		public string MaterialSlot2 { get; set; }

		public string AdditionalMaterial { get; set; }

		public bool IsEventEnhanceAShot { get; set; }

		public bool CheatPermission { get; set; }

		public EnhanceItemByData(long itemID, string materialSlot1, string materialSlot2, string additionalMaterial, bool isEvent, bool isCheat)
		{
			this.ItemID = itemID;
			this.MaterialSlot1 = materialSlot1;
			this.MaterialSlot2 = materialSlot2;
			this.AdditionalMaterial = additionalMaterial;
			this.IsEventEnhanceAShot = isEvent;
			this.CheatPermission = isCheat;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
