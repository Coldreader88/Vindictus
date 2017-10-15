using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetSkillElement
	{
		public PetSkillElement(int skillID, int slotOrder, int openLevel, DateTime? expiredTime)
		{
			this.SkillID = skillID;
			this.SlotOrder = slotOrder;
			this.OpenLevel = openLevel;
			if (expiredTime == null)
			{
				this.HasExpireDateTimeInfo = false;
				this.ExpireDateTimeDiff = -1L;
				return;
			}
			this.HasExpireDateTimeInfo = true;
			this.ExpireDateTimeDiff = expiredTime.Value.Ticks - DateTime.UtcNow.Ticks;
		}

		public int SkillID { get; set; }

		public int SlotOrder { get; set; }

		public int OpenLevel { get; set; }

		public bool HasExpireDateTimeInfo { get; set; }

		public long ExpireDateTimeDiff { get; set; }
	}
}
