using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MonsterTakeDamageInfo
	{
		public int Attacker { get; set; }

		public float AttackTime { get; set; }

		public string ActionName { get; set; }

		public int Damage { get; set; }

		public override string ToString()
		{
			return string.Format("MonsterTakeDamageInfo[ attacker = {0} attackTime = {1:0.00} actionName = {2} damage = {3} ]", new object[]
			{
				this.Attacker,
				this.AttackTime,
				this.ActionName,
				this.Damage
			});
		}
	}
}
