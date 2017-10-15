using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SkillEnhanceRestoreDurability : Operation
	{
		public long EnhanceStoneItemID { get; set; }

		public int UseCount { get; set; }

		public string SkillName { get; set; }

		public SkillEnhanceRestoreDurability(long enhanceStoneItemID, int useCount, string skillName)
		{
			this.EnhanceStoneItemID = enhanceStoneItemID;
			this.UseCount = useCount;
			this.SkillName = skillName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
