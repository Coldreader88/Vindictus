using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillListMessage : IMessage
	{
		public string LearningSkillID { get; private set; }

		public int CurrentAp { get; private set; }

		public ICollection<BriefSkillInfo> SkillList { get; private set; }

		public bool IsResetVocation { get; private set; }

		public SkillListMessage(string learningSkillID, int currentAp, ICollection<BriefSkillInfo> skillList, bool isResetVocation)
		{
			this.LearningSkillID = learningSkillID;
			this.CurrentAp = currentAp;
			this.SkillList = skillList;
			this.IsResetVocation = isResetVocation;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(string.Format("SkillListMessage [ learningSkillID = {0} currentAp = {1} vocationReset = {2} skillList = (", this.LearningSkillID, this.CurrentAp, this.IsResetVocation));
			foreach (BriefSkillInfo briefSkillInfo in this.SkillList)
			{
			}
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
