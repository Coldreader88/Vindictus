using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SpSkillMessage : IMessage
	{
		public IDictionary<int, string> SpSkills { get; set; }

		public SpSkillMessage(IDictionary<int, string> spSkills)
		{
			this.SpSkills = spSkills;
		}

		public override string ToString()
		{
			return string.Format("SpSkillMessage [ {0} ]", this.SpSkills.Count);
		}
	}
}
