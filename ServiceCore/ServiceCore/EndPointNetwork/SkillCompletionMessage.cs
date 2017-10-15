using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillCompletionMessage : IMessage
	{
		public string SkillID { get; set; }

		public int SkillRank { get; set; }

		public SkillCompletionMessage(string skillID, int skillRank)
		{
			this.SkillID = skillID;
			this.SkillRank = skillRank;
		}

		public override string ToString()
		{
			return string.Format("SkillCompletionMessage[ skillID = {0} Rank = {1} ]", this.SkillID, this.SkillRank);
		}
	}
}
