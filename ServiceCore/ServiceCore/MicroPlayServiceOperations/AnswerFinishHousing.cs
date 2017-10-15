using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class AnswerFinishHousing : Operation
	{
		public long CID { get; set; }

		public bool FollowHost { get; set; }

		public AnswerFinishHousing(long cid, bool followHost)
		{
			this.CID = cid;
			this.FollowHost = followHost;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
