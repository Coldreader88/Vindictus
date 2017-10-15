using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class GivePetSkillPoint : Operation
	{
		public int SkillPoint { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
