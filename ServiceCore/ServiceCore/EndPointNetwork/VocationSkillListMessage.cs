using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class VocationSkillListMessage : IMessage
	{
		public IDictionary<string, int> SkillList { get; private set; }

		public VocationSkillListMessage(IDictionary<string, int> skillList)
		{
			this.SkillList = skillList;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("VocationSkillListMessage [ ");
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
