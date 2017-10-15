using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class UpdateRankBasis : Operation
	{
		public long CID { get; private set; }

		public string RankID { get; private set; }

		public long Score { get; private set; }

		public long EntityID { get; private set; }

		public string GuildName { get; private set; }

		public UpdateRankBasis(long CID, string RankID, long Score)
		{
			this.CID = CID;
			this.RankID = RankID;
			this.Score = Score;
		}

		public UpdateRankBasis(long CID, string RankID, long Score, long EntityID)
		{
			this.CID = CID;
			this.RankID = RankID;
			this.Score = Score;
			this.EntityID = EntityID;
		}

		public UpdateRankBasis(long CID, string RankID, long Score, string GuildName)
		{
			this.CID = CID;
			this.RankID = RankID;
			this.Score = Score;
			this.EntityID = this.EntityID;
			this.GuildName = GuildName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
