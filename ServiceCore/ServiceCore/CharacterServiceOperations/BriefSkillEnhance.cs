using System;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class BriefSkillEnhance
	{
		public int GroupKey { get; set; }

		public int IndexKey { get; set; }

		public byte Type { get; set; }

		public int ReduceDurability { get; set; }

		public int MaxDurabilityBonus { get; set; }
	}
}
