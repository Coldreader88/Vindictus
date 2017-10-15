using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class CompleteSkill : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
