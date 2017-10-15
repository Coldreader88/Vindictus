using System;
using System.Collections.Generic;
using ServiceCore.MicroPlayServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DropErgMessage : IMessage
	{
		public int BrokenProp { get; set; }

		public string MonsterEntityName { get; set; }

		public List<ErgInfo> DropErg { get; set; }

		public DropErgMessage(int prop, List<ErgInfo> ergs)
		{
			this.BrokenProp = prop;
			this.MonsterEntityName = "";
			this.DropErg = ergs;
		}

		public DropErgMessage(string monsterEntityName, List<ErgInfo> ergs)
		{
			this.BrokenProp = -1;
			this.MonsterEntityName = monsterEntityName;
			this.DropErg = ergs;
		}

		public override string ToString()
		{
			return string.Format("DropErgMessage[ prop = {0} monster {1} erg x {2} ]", this.BrokenProp, this.MonsterEntityName, this.DropErg.Count);
		}
	}
}
