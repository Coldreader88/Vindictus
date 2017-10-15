using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SetSpSkill : Operation
	{
		public int SlotID { get; set; }

		public string SkillID { get; set; }

		public SetSpSkill(int slotID, string skillID)
		{
			this.SlotID = slotID;
			this.SkillID = skillID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
