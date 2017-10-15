using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillEnhanceUseErgMessage : IMessage
	{
		public long ErgItemID { get; set; }

		public int UseCount { get; set; }
	}
}
