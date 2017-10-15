using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DecomposeItemMessage : IMessage
	{
		public long ItemID { get; set; }

		public override string ToString()
		{
			return string.Format("DecomposeItemMessage[ TargetItemID = {0} ]", this.ItemID);
		}
	}
}
