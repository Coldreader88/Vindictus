using System;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class BlessStoneBonusInfo
	{
		public int EXPBonus { get; set; }

		public int APBonus { get; set; }

		public int LUCKBonus { get; set; }

		public int IgnoreFatigue { get; set; }

		public BlessStoneBonusInfo()
		{
			this.EXPBonus = 0;
			this.APBonus = 0;
			this.LUCKBonus = 0;
			this.IgnoreFatigue = 0;
		}

		public static BlessStoneBonusInfo operator +(BlessStoneBonusInfo x1, BlessStoneBonusInfo x2)
		{
			return new BlessStoneBonusInfo
			{
				EXPBonus = x1.EXPBonus + x2.EXPBonus,
				APBonus = x1.APBonus + x2.APBonus,
				LUCKBonus = x1.LUCKBonus + x2.LUCKBonus,
				IgnoreFatigue = x1.IgnoreFatigue + x2.IgnoreFatigue
			};
		}
	}
}
