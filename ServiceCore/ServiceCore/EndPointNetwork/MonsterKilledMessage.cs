using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MonsterKilledMessage : IMessage
	{
		public int Attacker { get; set; }

		public int Target { get; set; }

		public string ActionType { get; set; }

		public bool HasEvilCore { get; set; }

		public int Damage { get; set; }

		public int DamagePositionX { get; set; }

		public int DamagePositionY { get; set; }

		public int Distance { get; set; }

		public int ActorIndex { get; set; }

		public override string ToString()
		{
			return string.Format("MonsterKilledMessage[ attacker = {0} target = {1} actionType = {2} damage = {3} position = {4},{5} distance = {6}, Index = {7} ]", new object[]
			{
				this.Attacker,
				this.Target,
				this.ActionType,
				this.Damage,
				this.DamagePositionX,
				this.DamagePositionY,
				this.Distance,
				this.ActorIndex
			});
		}
	}
}
