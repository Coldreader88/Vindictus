using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class AnswerFinishQuest : Operation
	{
		public long CID { get; set; }

		public bool FollowHost { get; set; }

		public AnswerFinishQuest(long cid, bool followHost)
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
