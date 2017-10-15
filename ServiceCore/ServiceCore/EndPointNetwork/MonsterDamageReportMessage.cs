using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MonsterDamageReportMessage : IMessage
	{
		public int Target { get; set; }

		public ICollection<MonsterTakeDamageInfo> TakeDamageList { get; set; }
	}
}
