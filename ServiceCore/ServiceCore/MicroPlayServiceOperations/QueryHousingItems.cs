using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class QueryHousingItems : Operation
	{
		public long CID { get; set; }

		public QueryHousingItems(long cID)
		{
			this.CID = cID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
