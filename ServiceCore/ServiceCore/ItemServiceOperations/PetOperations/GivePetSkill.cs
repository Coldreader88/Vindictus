using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class GivePetSkill : Operation
	{
		public long PetID { get; set; }

		public int SkillSlotOrder { get; set; }

		public int OpenLevel { get; set; }

		public int SkillID { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
