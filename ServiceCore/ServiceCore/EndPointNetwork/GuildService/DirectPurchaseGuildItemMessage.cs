using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class DirectPurchaseGuildItemMessage : IMessage
	{
		public int ProductNo { get; set; }

		public bool IsCredit { get; set; }

		public string ItemClass { get; set; }

		public override string ToString()
		{
			return string.Format("", new object[0]);
		}
	}
}
