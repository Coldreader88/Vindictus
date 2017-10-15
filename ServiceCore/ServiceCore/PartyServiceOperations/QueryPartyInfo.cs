using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class QueryPartyInfo : Operation
	{
		public long PartyID
		{
			get
			{
				return this.partyID;
			}
			set
			{
				this.partyID = value;
			}
		}

		public int State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		public List<PartyMemberInfo> Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		public long MicroPlayID
		{
			get
			{
				return this.microPlayID;
			}
			set
			{
				this.microPlayID = value;
			}
		}

		public int PartySize
		{
			get
			{
				return this.partySize;
			}
			set
			{
				this.partySize = value;
			}
		}

		public List<long> FIDs
		{
			get
			{
				return this.frontendIDs;
			}
			set
			{
				this.frontendIDs = value;
			}
		}

		public List<long> CIDs
		{
			get
			{
				return this.cids;
			}
			set
			{
				this.cids = value;
			}
		}

		public bool QueryID { get; set; }

		public QueryPartyInfo(bool queryID)
		{
			this.QueryID = queryID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryPartyInfo.Request(this);
		}

		[NonSerialized]
		private long partyID;

		[NonSerialized]
		private int state;

		[NonSerialized]
		private List<PartyMemberInfo> members;

		[NonSerialized]
		private long microPlayID;

		[NonSerialized]
		private int partySize;

		[NonSerialized]
		private List<long> cids;

		[NonSerialized]
		private List<long> frontendIDs;

		private class Request : OperationProcessor<QueryPartyInfo>
		{
			public Request(QueryPartyInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is PartyInfo)
				{
					PartyInfo msg = base.Feedback as PartyInfo;
					base.Operation.Members = msg.Members;
					base.Operation.PartyID = msg.PartyID;
					base.Operation.State = msg.State;
					base.Operation.MicroPlayID = msg.MicroPlayID;
					base.Operation.PartySize = msg.PartySize;
					if (base.Operation.QueryID)
					{
						yield return null;
						base.Operation.CIDs = (base.Feedback as List<long>);
						yield return null;
						base.Operation.FIDs = (base.Feedback as List<long>);
					}
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
