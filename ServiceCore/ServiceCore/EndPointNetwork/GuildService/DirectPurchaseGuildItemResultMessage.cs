using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class DirectPurchaseGuildItemResultMessage : IMessage
	{
		public string ItemClass { get; set; }

		public int Result { get; set; }

		public DirectPurchaseGuildItemResultMessage(string itemClass, DirectPurchaseGuildItemResultCode result)
		{
			this.ItemClass = itemClass;
			this.Result = (int)result;
		}

		public override string ToString()
		{
			return string.Format("", new object[0]);
		}
	}
}
