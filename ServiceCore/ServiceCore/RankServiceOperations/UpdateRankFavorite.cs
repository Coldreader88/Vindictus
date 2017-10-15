using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class UpdateRankFavorite : Operation
	{
		public long CID { get; private set; }

		public string RankID { get; private set; }

		public bool IsAddition { get; private set; }

		public UpdateRankFavorite(long CID, string RankID, bool IsAddition)
		{
			this.CID = CID;
			this.RankID = RankID;
			this.IsAddition = IsAddition;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
