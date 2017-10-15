using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BurnGauge : IMessage
	{
		public int Gauge { get; set; }

		public int JackpotStartGauge { get; set; }

		public int JackpotMaxGauge { get; set; }

		public BurnGauge(int gauge, int jackpotStartGauge, int jackpotMaxGauge)
		{
			this.Gauge = gauge;
			this.JackpotStartGauge = jackpotStartGauge;
			this.JackpotMaxGauge = jackpotMaxGauge;
		}
	}
}
