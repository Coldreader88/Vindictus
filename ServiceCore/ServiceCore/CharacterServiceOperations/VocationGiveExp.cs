using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class VocationGiveExp : Operation
	{
		public int Exp { get; set; }

		public VocationGiveExp(int exp)
		{
			this.Exp = exp;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
