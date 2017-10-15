using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class QueryShipInfo : Operation
	{
		public ShipInfo ShipInfo
		{
			get
			{
				return this.shipInfo;
			}
			set
			{
				this.shipInfo = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryShipInfo.Request(this);
		}

		[NonSerialized]
		private ShipInfo shipInfo;

		private class Request : OperationProcessor<QueryShipInfo>
		{
			public Request(QueryShipInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.ShipInfo = (base.Feedback as ShipInfo);
				if (base.Operation.ShipInfo == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
