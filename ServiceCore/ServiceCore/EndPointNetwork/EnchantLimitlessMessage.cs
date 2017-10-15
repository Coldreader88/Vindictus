using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EnchantLimitlessMessage : IMessage
	{
		public long enchantID { get; set; }

		public long enchantLimitlessID { get; set; }

		public override string ToString()
		{
			return string.Format("EnchantLimitlessMessage[ BaseItemID = {0} LookItemID = {1}]", this.enchantID, this.enchantLimitlessID);
		}
	}
}
