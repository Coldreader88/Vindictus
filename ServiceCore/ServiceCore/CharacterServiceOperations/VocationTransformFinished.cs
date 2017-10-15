using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class VocationTransformFinished : Operation
	{
		public int TotalDamage { get; set; }

		public VocationTransformFinished(int totalDamage)
		{
			this.TotalDamage = totalDamage;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
