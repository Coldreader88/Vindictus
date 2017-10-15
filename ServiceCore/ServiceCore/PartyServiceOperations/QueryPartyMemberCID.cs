using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class QueryPartyMemberCID : Operation
	{
		public ICollection<PvPPartyMemberInfo> MemberCIDs
		{
			get
			{
				return this.memberCIDs;
			}
			set
			{
				this.memberCIDs = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryPartyMemberCID.Request(this);
		}

		[NonSerialized]
		private ICollection<PvPPartyMemberInfo> memberCIDs;

		private class Request : OperationProcessor<QueryPartyMemberCID>
		{
			public Request(QueryPartyMemberCID op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.MemberCIDs = (base.Feedback as IList<PvPPartyMemberInfo>);
				if (base.Operation.MemberCIDs == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
