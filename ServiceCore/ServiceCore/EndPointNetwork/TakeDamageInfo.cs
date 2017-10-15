using System;
using System.IO;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TakeDamageInfo
	{
		public int Victim
		{
			get
			{
				return this.victim;
			}
		}

		public int Attacker
		{
			get
			{
				return this.attacker;
			}
		}

		public int Action
		{
			get
			{
				return this.action;
			}
		}

		public int Damage
		{
			get
			{
				return this.damage;
			}
		}

		public int ResultHP
		{
			get
			{
				return this.resultHP;
			}
		}

		private void Deserialize(BinaryReader reader)
		{
			this.victim = reader.ReadInt32();
			this.attacker = reader.ReadInt32();
			this.action = reader.ReadInt32();
			this.damage = reader.ReadInt32();
			this.resultHP = reader.ReadInt32();
		}

		public override string ToString()
		{
			return string.Format("TakeDamageInfo( victim = {0} attacker = {1} action = {2} damage = {3} resultHP = {4} ]", new object[]
			{
				this.victim,
				this.attacker,
				this.action,
				this.damage,
				this.resultHP
			});
		}

		private int victim;

		private int attacker;

		private int action;

		private int damage;

		private int resultHP;
	}
}
