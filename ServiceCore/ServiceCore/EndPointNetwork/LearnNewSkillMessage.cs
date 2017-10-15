using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LearnNewSkillMessage : IMessage
	{
		public string SkillID { get; set; }

		public LearnNewSkillMessage(string skillID)
		{
			this.SkillID = skillID;
		}

		public override string ToString()
		{
			return string.Format("LearnNewSkillMessage[skillID = {0}]", this.SkillID);
		}
	}
}
