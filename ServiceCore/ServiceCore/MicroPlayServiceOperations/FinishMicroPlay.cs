using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class FinishMicroPlay : Operation
	{
		public long CID { get; set; }

		public FinishMicroPlayType FinishType { get; set; }

		public FinishMicroPlay(long cid, FinishMicroPlayType type)
		{
			this.CID = cid;
			this.FinishType = type;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
