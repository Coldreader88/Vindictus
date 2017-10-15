using System;
using System.Collections.Generic;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class BriefSkillInfo
	{
		public string SkillID { get; set; }

		public int BaseRank { get; set; }

		public int FinalRank { get; set; }

		public int RequiredAP { get; set; }

		public byte IsLocked { get; set; }

		public byte CanStartTraining { get; set; }

		public int CurrentAP { get; set; }

		public int ResetSkillAP { get; set; }

		public int UntrainSkillAP { get; set; }

		public List<BriefSkillEnhance> Enhances { get; set; }

		public BriefSkillInfo()
		{
			this.Enhances = new List<BriefSkillEnhance>();
		}
	}
}
