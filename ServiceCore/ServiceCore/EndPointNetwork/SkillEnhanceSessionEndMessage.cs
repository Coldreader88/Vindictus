using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillEnhanceSessionEndMessage : IMessage
	{
		public string SkillName { get; set; }
	}
}
