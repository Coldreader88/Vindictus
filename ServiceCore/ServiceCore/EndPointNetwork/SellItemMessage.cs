using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SellItemMessage : IMessage
	{
		public long ItemID { get; set; }

		public int Count { get; set; }

		public override string ToString()
		{
			return string.Format("SellItemMessage[ slot = {0} ]", this.ItemID);
		}
	}
}
