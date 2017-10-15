using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ShipInfoMessage : IMessage
	{
		public ShipInfo ShipInfo { get; set; }

		public ShipInfoMessage()
		{
		}

		public ShipInfoMessage(ShipInfo sinfo)
		{
			this.ShipInfo = sinfo;
		}

		public override string ToString()
		{
			return string.Format("ShipInfoMessage[ {0} ]", this.ShipInfo);
		}
	}
}
