using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EnchantItemMessage : IMessage
	{
		public bool OpenNewSession { get; set; }

		public long TargetItemID { get; set; }

		public long EnchantScrollItemID { get; set; }

		public long IndestructibleScrollItemID { get; set; }

		public string DiceItemClass { get; set; }

		public override string ToString()
		{
			return string.Format("EnchantItemMessage[ OpenNewSession = {0} TargetItemID = {1} EnchantScrollItemID = {2} IndestructibleScrollItemID = {3} DiceItemClass = {4}]", new object[]
			{
				this.OpenNewSession,
				this.TargetItemID,
				this.EnchantScrollItemID,
				this.IndestructibleScrollItemID,
				this.DiceItemClass
			});
		}
	}
}
