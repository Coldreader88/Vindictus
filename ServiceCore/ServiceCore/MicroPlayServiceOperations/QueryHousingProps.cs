using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class QueryHousingProps : Operation
	{
		public long CID { get; set; }

		public QueryHousingProps(long cID)
		{
			this.CID = cID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
