using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class PurchaseGuildStorageMessage : IMessage
	{
		public int ProductNo { get; set; }

		public bool IsCredit { get; set; }

		public override string ToString()
		{
			return string.Format("PurchaseGuildStorageMessage[]", new object[0]);
		}
	}
}
