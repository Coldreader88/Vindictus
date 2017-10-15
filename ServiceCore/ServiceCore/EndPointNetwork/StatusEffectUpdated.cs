using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StatusEffectUpdated : IMessage
	{
		public string CharacterName { get; set; }

		public List<StatusEffectElement> StatusEffects { get; set; }

		public StatusEffectUpdated(string characterName, List<StatusEffectElement> effects)
		{
			this.CharacterName = characterName;
			this.StatusEffects = effects;
		}
	}
}
