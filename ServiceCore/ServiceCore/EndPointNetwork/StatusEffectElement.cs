using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StatusEffectElement
	{
		public string Type { get; set; }

		public int Level { get; set; }

		public int RemainTime { get; set; }

		public int CombatCount { get; set; }

		public StatusEffectElement(string type, int level, int remainTime, int combatCount)
		{
			this.Type = type;
			this.Level = level;
			this.RemainTime = remainTime;
			this.CombatCount = combatCount;
		}
	}
}
