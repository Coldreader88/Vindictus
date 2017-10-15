using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DyeItemCashMessage : IMessage
	{
		public long ItemID { get; set; }

		public long AmpleID { get; set; }

		public int Part { get; set; }

		public override string ToString()
		{
			return string.Format("DyeItemCashMessage[ ItemID = {0} AmpleID = {1} Part = {2} ]", this.ItemID, this.AmpleID, this.Part);
		}
	}
}
