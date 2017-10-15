using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillEnhanceChangedMessage : IMessage
	{
		public IDictionary<string, BriefSkillEnhance> SkillEnhanceChanged { get; set; }

		public SkillEnhanceChangedMessage(IDictionary<string, BriefSkillEnhance> skillEnhanceChanged)
		{
			this.SkillEnhanceChanged = skillEnhanceChanged;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(string.Format("SkillListMessage [(", new object[0]));
			foreach (KeyValuePair<string, BriefSkillEnhance> keyValuePair in this.SkillEnhanceChanged)
			{
			}
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
