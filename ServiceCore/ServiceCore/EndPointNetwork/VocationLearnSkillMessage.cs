using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class VocationLearnSkillMessage : IMessage
	{
		public string SkillID { get; set; }

		public override string ToString()
		{
			return string.Format("VocationLearnSkillMessage [ {0} ]", this.SkillID);
		}
	}
}
