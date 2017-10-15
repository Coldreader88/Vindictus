using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillEnhanceRestoreDurabilityMessage : IMessage
	{
		public long EnhanceStoneItemID { get; set; }

		public int UseCount { get; set; }

		public string SkillName { get; set; }

		public SkillEnhanceRestoreDurabilityMessage(long enhanceStoneItemID, int useCount, string skillName)
		{
			this.EnhanceStoneItemID = enhanceStoneItemID;
			this.UseCount = useCount;
			this.SkillName = skillName;
		}
	}
}
