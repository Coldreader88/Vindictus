using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class PartyInfo
	{
		public List<PartyMemberInfo> Members { get; set; }

		public long PartyID { get; set; }

		public int State { get; set; }

		public long MicroPlayID { get; set; }

		public int PartySize { get; set; }
	}
}
