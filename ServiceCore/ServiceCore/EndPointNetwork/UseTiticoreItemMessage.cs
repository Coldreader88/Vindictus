using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseTiticoreItemMessage : IMessage
	{
		public long ItemID { get; set; }

		public string TargetItemClass { get; set; }

		public UseTiticoreItemMessage(long itemID, string targetItemClass)
		{
			this.ItemID = itemID;
			this.TargetItemClass = targetItemClass;
		}

		public override string ToString()
		{
			return string.Format("UseTiticoreItemMessage[ ItemID = {0}, TargetItemClass = {1} ]", this.ItemID, this.TargetItemClass);
		}
	}
}
