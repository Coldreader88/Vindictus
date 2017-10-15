using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SpawnMonsterMessage : IMessage
	{
		public List<SpawnedMonster> MonsterList { get; set; }
	}
}
