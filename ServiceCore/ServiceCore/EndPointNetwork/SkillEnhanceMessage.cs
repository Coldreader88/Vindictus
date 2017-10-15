using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillEnhanceMessage : IMessage
	{
		public List<long> AdditionalItemIDs { get; set; }
	}
}
