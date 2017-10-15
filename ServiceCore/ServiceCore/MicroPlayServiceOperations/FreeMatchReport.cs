using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class FreeMatchReport : Operation
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

		public FreeMatchReport(int winnerTag, int loserTag)
		{
			this.winnerTag = winnerTag;
			this.loserTag = loserTag;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private int winnerTag;

		private int loserTag;
	}
}
