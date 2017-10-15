using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CombatRecordMessage : IMessage
	{
		public int PlayerNumber { get; set; }

		public int ComboMax { get; set; }

		public int HitMax { get; set; }

		public int StyleMax { get; set; }

		public int Death { get; set; }

		public int Kill { get; set; }

		public int BattleAchieve { get; set; }

		public int HitTake { get; set; }

		public int StyleCount { get; set; }

		public int RankStyle { get; set; }

		public int RankBattle { get; set; }

		public int RankTotal { get; set; }

		public override string ToString()
		{
			return string.Format("CombatRecordMessage [ PlayerNumber = {11}, ComboMax = {0}, HitMax = {1}, StyleMax = {2}, Death={3}, Kill={4}, BattleAchieve={5}, HitTake={6}, StyleCount={7}, RankStyle={8} RankBattle={9}, RankTotal={10} ]", new object[]
			{
				this.ComboMax,
				this.HitMax,
				this.StyleMax,
				this.Death,
				this.Kill,
				this.BattleAchieve,
				this.HitTake,
				this.StyleCount,
				this.RankStyle,
				this.RankBattle,
				this.RankTotal,
				this.PlayerNumber
			});
		}
	}
}
