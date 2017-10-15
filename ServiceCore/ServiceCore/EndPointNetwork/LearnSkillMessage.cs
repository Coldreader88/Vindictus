using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LearnSkillMessage : IMessage
	{
		public string SkillID
		{
			get
			{
				return this.skillID;
			}
		}

		public int AP
		{
			get
			{
				return this.ap;
			}
		}

		public LearnSkillMessage(string skillID, int ap)
		{
			this.skillID = skillID;
			this.ap = ap;
		}

		public override string ToString()
		{
			return string.Format("LearnSkillMessage[ ap = {0} ]", this.ap);
		}

		private string skillID;

		private int ap;
	}
}
