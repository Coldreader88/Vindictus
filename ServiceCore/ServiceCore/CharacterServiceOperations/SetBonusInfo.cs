using System;
using System.Collections.Generic;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SetBonusInfo
	{
		public IDictionary<string, int> SetSkillBonus { get; set; }

		public CharacterStats SetStatBonus { get; set; }
	}
}
