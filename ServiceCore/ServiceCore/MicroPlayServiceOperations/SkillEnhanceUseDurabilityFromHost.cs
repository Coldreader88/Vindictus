using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public class SkillEnhanceUseDurabilityFromHost : Operation
	{
		public int Slot { get; set; }

		public Dictionary<string, int> UseDurability { get; set; }

		public SkillEnhanceUseDurabilityFromHost(int slot, Dictionary<string, int> useDurability)
		{
			this.Slot = slot;
			this.UseDurability = useDurability;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
