using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ApplyMicroPlayEffectsMessage : IMessage
	{
		public int Caster { get; set; }

		public string EffectName { get; set; }

		public List<MicroPlayEffect> EffectList { get; set; }

		public override string ToString()
		{
			return string.Format("GoddessProtectionMessage", new object[0]);
		}
	}
}
