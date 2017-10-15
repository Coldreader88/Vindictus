using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class UpdateParty : Operation
	{
		public long PartyID { get; set; }

		public ICollection<PartyMemberInfo> Members { get; set; }

		public PartyInfoState State { get; set; }

		public int PartySize { get; set; }

		public bool IsMaster { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
