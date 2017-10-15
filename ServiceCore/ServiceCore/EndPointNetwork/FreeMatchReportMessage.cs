using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class FreeMatchReportMessage : IMessage
	{
		public int WinnerTag
		{
			get
			{
				return this.winnerTag;
			}
		}

		public int LoserTag
		{
			get
			{
				return this.loserTag;
			}
		}

		public FreeMatchReportMessage(int winnerTag, int loserTag)
		{
			this.winnerTag = winnerTag;
			this.loserTag = loserTag;
		}

		public override string ToString()
		{
			return string.Format("FreeMatchReportMessage[ winnerTag = {0}, loserTag = {0} ]", this.winnerTag, this.loserTag);
		}

		private int winnerTag;

		private int loserTag;
	}
}
