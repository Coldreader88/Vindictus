using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public class PickSharedItemMessage : IMessage
	{
		public long ItemID { get; set; }

		public int Amount { get; set; }

		public byte TargetTab { get; set; }

		public int TargetSlot { get; set; }

		public override string ToString()
		{
			return string.Format("PickSharedItemMessage[ {0} / {1} / {2} ]", this.ItemID, this.TargetTab, this.TargetSlot);
		}
	}
}
