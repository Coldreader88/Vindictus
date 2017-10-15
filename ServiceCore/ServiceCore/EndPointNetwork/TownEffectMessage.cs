using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TownEffectMessage : IMessage
	{
		public int EffectID { get; private set; }

		public int Duration { get; private set; }

		public TownEffectMessage(int effectID, int duration)
		{
			this.EffectID = effectID;
			this.Duration = duration;
		}

		public override string ToString()
		{
			return string.Format("TownEffectMessage[ EffectID {0} {1} ]", this.EffectID, this.Duration);
		}
	}
}
