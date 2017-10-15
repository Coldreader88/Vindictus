using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class AltarStatusEffect : Operation
	{
		public string StatusEffectName { get; set; }

		public int Level { get; set; }

		public int RemainTime { get; set; }

		public AltarStatusEffect(string statusEffectName, int level, int remainTime)
		{
			this.StatusEffectName = statusEffectName;
			this.Level = level;
			this.RemainTime = remainTime;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new AltarStatusEffect.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(AltarStatusEffect op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Result = (base.Feedback is OkMessage);
				yield break;
			}
		}
	}
}
