using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SkillEnhanceSessionEnd : Operation
	{
		public string SkillName { get; set; }

		public SkillEnhanceSessionEnd(string skillName)
		{
			this.SkillName = skillName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
