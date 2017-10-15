using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SkillEnhanceSessionStart : Operation
	{
		public string SkillName { get; set; }

		public long SkillEnhanceStoneItemID { get; set; }

		public SkillEnhanceSessionStart(string skillName, long stoneItemID)
		{
			this.SkillName = skillName;
			this.SkillEnhanceStoneItemID = stoneItemID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
