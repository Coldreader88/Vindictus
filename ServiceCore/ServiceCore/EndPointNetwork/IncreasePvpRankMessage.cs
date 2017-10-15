using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class IncreasePvpRankMessage : IMessage
	{
		public string RankID { get; set; }

		public int RankScore { get; set; }

		public int TestGuildID { get; set; }

		public string TestGuildName { get; set; }

		public override string ToString()
		{
			return string.Format("IncreasePvpRankMessage {0} {1} {2} {3}", new object[]
			{
				this.RankID,
				this.RankScore,
				this.TestGuildID,
				this.TestGuildName
			});
		}
	}
}
