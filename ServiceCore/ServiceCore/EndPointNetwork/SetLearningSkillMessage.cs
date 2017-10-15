using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetLearningSkillMessage : IMessage
	{
		public string SkillID
		{
			get
			{
				return this.skillID;
			}
		}

		public SetLearningSkillMessage(string skillID)
		{
			this.skillID = skillID;
		}

		public override string ToString()
		{
			return string.Format("SetLearningSkillMessage[ skillID = {0} ]", this.skillID);
		}

		private string skillID;
	}
}
