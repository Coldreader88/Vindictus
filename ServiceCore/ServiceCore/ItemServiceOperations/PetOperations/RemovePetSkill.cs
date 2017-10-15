using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class RemovePetSkill : Operation
	{
		public long PetID { get; set; }

		public int PetSkillType { get; set; }

		public RemovePetSkill(long petID, int petSkillType)
		{
			this.PetID = petID;
			this.PetSkillType = petSkillType;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
