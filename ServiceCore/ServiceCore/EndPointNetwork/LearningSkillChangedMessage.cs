using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LearningSkillChangedMessage : IMessage
	{
		public string SkillID { get; set; }

		public int AP { get; set; }

		public LearningSkillChangedMessage()
		{
		}

		public LearningSkillChangedMessage(string SkillID, int AP)
		{
			this.SkillID = SkillID;
			this.AP = AP;
		}

		public override string ToString()
		{
			return string.Format("{0}[ {1} ]", base.ToString(), this.SkillID);
		}
	}
}
