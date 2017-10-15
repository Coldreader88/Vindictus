using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MicroStatusEffectUpdated : IMessage
	{
		public string CharacterName { get; set; }

		public int Slot { get; set; }

		public List<StatusEffectElement> StatusEffects { get; set; }

		public MicroStatusEffectUpdated(string characterName, int slot, List<StatusEffectElement> effects)
		{
			this.CharacterName = characterName;
			this.Slot = slot;
			this.StatusEffects = effects;
		}
	}
}
