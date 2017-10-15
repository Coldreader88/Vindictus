using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class AddGuildStorageItemMessage : IMessage
	{
		public long ItemID { get; set; }

		public string ItemClass { get; set; }

		public int Amount { get; set; }

		public byte TargetTab { get; set; }

		public int TargetSlot { get; set; }

		public override string ToString()
		{
			return string.Format("AddGuildStorageItemMessage[]", new object[0]);
		}
	}
}
