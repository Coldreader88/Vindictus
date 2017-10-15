using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class ArrangeGuildStorageItemMessage : IMessage
	{
		public long ItemID { get; set; }

		public int Slot { get; set; }

		public override string ToString()
		{
			return string.Format("ArrangeGuildStorageItemMessage[]", new object[0]);
		}
	}
}
