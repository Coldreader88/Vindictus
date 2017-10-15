using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class VocationLearnSkill : Operation
	{
		public string VocationSkillID { get; set; }

		public VocationLearnSkill(string vocationSkillID)
		{
			this.VocationSkillID = vocationSkillID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
