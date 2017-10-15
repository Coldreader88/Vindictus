using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class TownEffectOperation : Operation
	{
		public int EffectID { get; set; }

		public int Duration { get; set; }

		public TownEffectOperation(int effectID, int duration)
		{
			this.EffectID = effectID;
			this.Duration = duration;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
