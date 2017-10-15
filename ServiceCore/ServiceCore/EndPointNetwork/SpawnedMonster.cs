using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SpawnedMonster
	{
		public int EntityID { get; set; }

		public string Point { get; set; }

		public string Model { get; set; }

		public int VarianceBefore { get; set; }

		public int VarianceAfter { get; set; }

		public int Speed { get; set; }

		public int MonsterType { get; set; }

		public string AIType { get; set; }

		public string MonsterVariance { get; set; }
	}
}
