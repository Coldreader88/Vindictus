using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CafeLoginMessage : IMessage
	{
		public CafeLoginMessage(int homeCafeRemainTime, int accountHomeCafeRemainTime, int cafeType)
		{
			this.HomeCafeRemainTime = homeCafeRemainTime;
			this.AccountHomeCafeRemainTime = accountHomeCafeRemainTime;
			this.CafeType = cafeType;
		}

		private int HomeCafeRemainTime;

		private int AccountHomeCafeRemainTime;

		private int CafeType;
	}
}
