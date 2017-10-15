using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class JoinShipMessage : IMessage
	{
		public long ShipID { get; set; }

		public bool IsAssist { get; set; }

		public bool IsInTownWithShipInfo { get; set; }

		public bool IsDedicatedServer { get; set; }

		public bool IsNewbieRecommend { get; set; }

		public override string ToString()
		{
			return string.Format("JoinShipMessage [ ShipID : {0}, IsAssist : {1}, IsNewbieRecommend : {2} ]", this.ShipID, this.IsAssist, this.IsNewbieRecommend);
		}
	}
}
