using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetStatusInfo
	{
		public long PetID { get; set; }

		public string PetName { get; set; }

		public int PetType { get; set; }

		public int Slot { get; set; }

		public int Level { get; set; }

		public int Exp { get; set; }

		public int Desire { get; set; }

		public PetStatElement Stat { get; set; }

		public List<PetSkillElement> Skills { get; set; }

		public List<PetAccessoryElement> Accessories { get; set; }

		public byte PetStatus { get; set; }

		public int RemainActiveTime { get; set; }

		public DateTime RemainExpiredTime { get; set; }

		public PetStatusInfo()
		{
		}

		public PetStatusInfo(PetStatusInfo rhs)
		{
			this.PetID = rhs.PetID;
			this.PetName = rhs.PetName;
			this.PetType = rhs.PetType;
			this.Slot = rhs.Slot;
			this.Level = rhs.Level;
			this.Exp = rhs.Exp;
			this.Desire = rhs.Desire;
			this.Stat = new PetStatElement(rhs.Stat);
			this.Skills = new List<PetSkillElement>(rhs.Skills);
			this.Accessories = new List<PetAccessoryElement>(rhs.Accessories);
			this.PetStatus = rhs.PetStatus;
			this.RemainActiveTime = rhs.RemainActiveTime;
			this.RemainExpiredTime = rhs.RemainExpiredTime;
		}

		public override string ToString()
		{
			return string.Format("PetStatusInfo >>> PetID: {0}, Name: {1}, Status: {2}, ActiveTime: {3} ", new object[]
			{
				this.PetID,
				this.PetName,
				this.PetStatus,
				this.RemainActiveTime
			});
		}
	}
}
