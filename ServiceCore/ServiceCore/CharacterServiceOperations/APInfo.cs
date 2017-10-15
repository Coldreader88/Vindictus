using System;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class APInfo
	{
		public long NextBonusTimeTicks { get; set; }

		public int APBonusInterval { get; set; }

		public int AP { get; set; }

		public int MaxAP { get; set; }

		public APInfo(int ap, int maxAP, long nextBonusTimeTicks, int apBonusInterval)
		{
			this.AP = ap;
			this.MaxAP = maxAP;
			this.NextBonusTimeTicks = nextBonusTimeTicks;
			this.APBonusInterval = apBonusInterval;
		}
	}
}
