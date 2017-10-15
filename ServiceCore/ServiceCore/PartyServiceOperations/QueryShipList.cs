using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class QueryShipList : Operation
	{
		public LinkedList<ShipInfo> ShipList
		{
			get
			{
				return this.shipList;
			}
		}

		public int PlayerLevel { get; set; }

		public IDictionary<string, int> InterestedQuests { get; set; }

		public QueryShipList()
		{
			this.InterestedQuests = new Dictionary<string, int>();
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryShipList.Request(this);
		}

		[NonSerialized]
		private LinkedList<ShipInfo> shipList;

		private class Request : OperationProcessor<QueryShipList>
		{
			public Request(QueryShipList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.shipList = (base.Feedback as LinkedList<ShipInfo>);
				if (base.Operation.shipList == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
