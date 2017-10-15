using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class OpenTiticoreUIMessage : IMessage
	{
		public long ItemID { get; set; }

		public string TargetItemClass { get; set; }

		public override string ToString()
		{
			return string.Format("OpenTiticoreUIMessage[ itemID = {0}, TargetItemClass = {1}]", this.ItemID, this.TargetItemClass);
		}
	}
}
