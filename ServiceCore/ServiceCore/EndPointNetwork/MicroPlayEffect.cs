using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MicroPlayEffect
	{
		public int PlayerSlotNo { get; set; }

		public MicroPlayEffectType Effect { get; set; }

		public int Amount { get; set; }

		public string Argument { get; set; }
	}
}
