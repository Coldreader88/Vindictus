using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DropGoldCoreMessage : IMessage
	{
		public string MonsterEntityName { get; set; }

		public List<EvilCoreInfo> EvilCores { get; set; }

		public int DropImmediately { get; set; }

		public DropGoldCoreMessage(string monsterEntityName, List<EvilCoreInfo> list, int dropImmediately)
		{
			this.MonsterEntityName = monsterEntityName;
			this.EvilCores = list;
			this.DropImmediately = dropImmediately;
		}

		public override string ToString()
		{
			return string.Format("DropGoldCoreMessage[ {0}x{1} ]", this.MonsterEntityName, this.EvilCores.Count);
		}
	}
}
