using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillEnhanceSessionStartMessage : IMessage
	{
		public string SkillName { get; set; }

		public long SkillEnhanceStoneItemID { get; set; }
	}
}
