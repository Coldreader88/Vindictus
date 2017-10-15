using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SkillEnhanceSessionUseErg : Operation
	{
		public long ErgItemID { get; set; }

		public int UseCount { get; set; }

		public SkillEnhanceSessionUseErg(long ergItemID, int useCount)
		{
			this.ErgItemID = ergItemID;
			this.UseCount = useCount;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
