using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class UseAdvancedFeatherMessage : IMessage
	{
		public long ItemID { get; set; }

		public string TargetName { get; set; }

		public override string ToString()
		{
			return string.Format("UseAdvancedFeatherMessage[ itemID = {0}, TargetName = {1} ]", this.ItemID, this.TargetName);
		}
	}
}
