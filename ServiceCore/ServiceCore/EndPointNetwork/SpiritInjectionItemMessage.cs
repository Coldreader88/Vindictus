using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SpiritInjectionItemMessage : IMessage
	{
		public long SpiritStoneID { get; set; }

		public long TargetItemID { get; set; }

		public override string ToString()
		{
			return string.Format("SpiritInjectionItemMessage[ SpiritStoneID = {0} TargetItemID = {1} ]", this.SpiritStoneID, this.TargetItemID);
		}
	}
}
