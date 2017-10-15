using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class InformCombatRecord : Operation
	{
		public int PlayerNum { get; set; }

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

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
