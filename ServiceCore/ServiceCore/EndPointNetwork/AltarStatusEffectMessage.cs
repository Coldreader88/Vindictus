using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AltarStatusEffectMessage : IMessage
	{
		public int Type { get; set; }

		public override string ToString()
		{
			return string.Format("AltarStatusEffectMessage {0}", this.Type);
		}
	}
}
