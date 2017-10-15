using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class APMessage : IMessage
	{
		public long NextBonusTimeTicks
		{
			get
			{
				return this.nextBonusTimeTicks;
			}
		}

		public int AP
		{
			get
			{
				return this.ap;
			}
		}

		public int MaxAP
		{
			get
			{
				return this.maxap;
			}
		}

		public int APBonusInterval
		{
			get
			{
				return this.apBonusInterval;
			}
		}

		public APMessage(int ap, int maxAP, long nextBonusTimeTicks, int apBonusInterval)
		{
			this.ap = ap;
			this.maxap = maxAP;
			this.nextBonusTimeTicks = nextBonusTimeTicks;
			this.apBonusInterval = apBonusInterval;
		}

		public override string ToString()
		{
			return string.Format("APMessage [ ap = {0} maxAP = {1} nextBonusTimeTicks = {2} apBonusInterval = {3} ]", new object[]
			{
				this.ap,
				this.maxap,
				this.nextBonusTimeTicks,
				this.apBonusInterval
			});
		}

		private long nextBonusTimeTicks;

		private int ap;

		private int maxap;

		private int apBonusInterval;
	}
}
