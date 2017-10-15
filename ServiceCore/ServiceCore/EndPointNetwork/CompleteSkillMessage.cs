using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CompleteSkillMessage : IMessage
	{
		public override string ToString()
		{
			return "CompleteSkillMessage";
		}
	}
}
