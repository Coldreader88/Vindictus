using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class RegisterHousingInfo : Operation
	{
		public long CID { get; set; }

		public long Index { get; set; }

		public RegisterHousingInfo(long cid, int index)
		{
			this.CID = cid;
			this.Index = (long)index;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
