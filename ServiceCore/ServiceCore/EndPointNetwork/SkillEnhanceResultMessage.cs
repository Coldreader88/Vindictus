using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillEnhanceResultMessage : IMessage
	{
		public string SkillName { get; set; }

		public string SkillEnhanceStoneItem { get; set; }

		public int SuccessRatio { get; set; }

		public List<string> AdditionalItemClasses { get; set; }

		public bool Result { get; set; }

		public bool IsAdditionalItemDestroyed { get; set; }

		public bool IsEnhanceStoneProtected { get; set; }

		public List<BriefSkillEnhance> Enhances { get; set; }

		public SkillEnhanceResultMessage(bool result, string skillName, List<string> additionalItemClasses, int successRatio, string skillEnhanceStoenItemClassEx, bool isAdditionalItemDestroyed, bool isEnhanceStoneProtected, List<BriefSkillEnhance> enhances)
		{
			this.Result = result;
			this.SkillName = skillName;
			this.SkillEnhanceStoneItem = skillEnhanceStoenItemClassEx;
			this.SuccessRatio = successRatio;
			this.AdditionalItemClasses = additionalItemClasses;
			this.IsAdditionalItemDestroyed = isAdditionalItemDestroyed;
			this.IsEnhanceStoneProtected = isEnhanceStoneProtected;
			this.Enhances = enhances;
		}
	}
}
