using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetStatElement
	{
		public PetStatElement(int requiredExp, int maxExp, int hp, int resDamage, int hpRecovery, int defBreak, int atkBalance, int atk, int def, int critical, int resCritical)
		{
			this.RequiredExp = requiredExp;
			this.MaxExp = maxExp;
			this.Hp = hp;
			this.ResDamage = resDamage;
			this.HpRecovery = hpRecovery;
			this.DefBreak = defBreak;
			this.AtkBalance = atkBalance;
			this.Atk = atk;
			this.Def = def;
			this.Critical = critical;
			this.ResCritical = resCritical;
		}

		public PetStatElement(PetStatElement rhs)
		{
			this.RequiredExp = rhs.RequiredExp;
			this.MaxExp = rhs.MaxExp;
			this.Hp = rhs.Hp;
			this.ResDamage = rhs.ResDamage;
			this.HpRecovery = rhs.HpRecovery;
			this.DefBreak = rhs.DefBreak;
			this.AtkBalance = rhs.AtkBalance;
			this.Atk = rhs.Atk;
			this.Def = rhs.Def;
			this.Critical = rhs.Critical;
			this.ResCritical = rhs.ResCritical;
		}

		public int RequiredExp { get; set; }

		public int MaxExp { get; set; }

		public int Hp { get; set; }

		public int ResDamage { get; set; }

		public int HpRecovery { get; set; }

		public int DefBreak { get; set; }

		public int AtkBalance { get; set; }

		public int Atk { get; set; }

		public int Def { get; set; }

		public int Critical { get; set; }

		public int ResCritical { get; set; }
	}
}
