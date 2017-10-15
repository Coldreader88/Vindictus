using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SharingCheckMessage : IMessage
	{
		public string SharingCharacterName { get; set; }

		public string ItemClassName { get; set; }

		public string StatusEffect { get; set; }

		public int EffectLevel { get; set; }

		public int DurationSec { get; set; }
	}
}
