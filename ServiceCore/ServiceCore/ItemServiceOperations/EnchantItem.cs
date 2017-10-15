using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class EnchantItem : Operation
	{
		public bool OpenNewSession { get; set; }

		public long TargetItemID { get; set; }

		public long EnchantScrollItemID { get; set; }

		public long IndestructibleScrollItemID { get; set; }

		public string DiceItemClass { get; set; }

		public EnchantItem(bool openNewSession, long targetItemID, long enchantScrollItemID, long indestructibleScrollItemID, string diceItemClass)
		{
			this.OpenNewSession = openNewSession;
			this.TargetItemID = targetItemID;
			this.EnchantScrollItemID = enchantScrollItemID;
			this.IndestructibleScrollItemID = indestructibleScrollItemID;
			this.DiceItemClass = diceItemClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
