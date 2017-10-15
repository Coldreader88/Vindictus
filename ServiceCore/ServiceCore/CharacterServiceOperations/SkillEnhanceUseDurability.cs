using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SkillEnhanceUseDurability : Operation
	{
		public Dictionary<string, int> UseDurability { get; set; }

		public SkillEnhanceUseDurability(Dictionary<string, int> useDurability)
		{
			this.UseDurability = useDurability;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
