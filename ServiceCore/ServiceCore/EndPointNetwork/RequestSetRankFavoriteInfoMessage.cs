using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestSetRankFavoriteInfoMessage : IMessage
	{
		public long CID { get; set; }

		public string RankID { get; set; }

		public bool IsAddition { get; set; }

		public override string ToString()
		{
			return string.Format("RequestSetRankFavoriteInfoMessage {0} {1} {2}", this.CID, this.RankID, this.IsAddition ? "Insert" : "Delete");
		}
	}
}
