using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillEnhanceUseDurabilityMessage : IMessage
	{
		public int Slot { get; set; }

		public Dictionary<string, int> UseDurability { get; set; }

		public SkillEnhanceUseDurabilityMessage(int slot, Dictionary<string, int> useDurability)
		{
			this.Slot = slot;
			this.UseDurability = useDurability;
		}
	}
}
