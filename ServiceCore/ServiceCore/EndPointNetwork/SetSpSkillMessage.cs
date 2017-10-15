using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetSpSkillMessage : IMessage
	{
		public int SlotID { get; set; }

		public string SkillID { get; set; }

		public SetSpSkillMessage(int slotID, string skillID)
		{
			this.SlotID = slotID;
			this.SkillID = skillID;
		}

		public override string ToString()
		{
			return string.Format("SetSpSkillMessage [ {0} : {1} ]", this.SlotID, this.SkillID);
		}
	}
}
