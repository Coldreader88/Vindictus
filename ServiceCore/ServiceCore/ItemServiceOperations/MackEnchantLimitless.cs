using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class MackEnchantLimitless : Operation
	{
		public long EnchantID { get; set; }

		public long EnchantLimitlessID { get; set; }

		public MackEnchantLimitless(long enchantID, long enchantLimitlessID)
		{
			this.EnchantID = enchantID;
			this.EnchantLimitlessID = enchantLimitlessID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
