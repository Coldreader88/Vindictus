using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class FinishHousingPlay : Operation
	{
		public long CID { get; set; }

		public FinishHousingPlay(long cid)
		{
			this.CID = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
