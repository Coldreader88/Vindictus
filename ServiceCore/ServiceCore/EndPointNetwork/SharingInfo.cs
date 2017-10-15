using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SharingInfo
	{
		public int EffectLevel { get; set; }

		public int DurationSec { get; set; }

		public string ItemClassName { get; set; }

		public string StatusEffect { get; set; }

		public override string ToString()
		{
			return string.Format("SharingInfo({0}, {1}, {2}, {3})", new object[]
			{
				this.ItemClassName,
				this.StatusEffect,
				this.EffectLevel,
				this.DurationSec
			});
		}
	}
}
