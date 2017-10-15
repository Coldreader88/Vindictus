using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ReleaseLearningSkillMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ReleaseLearningSkillMessage[ ]", new object[0]);
		}
	}
}
