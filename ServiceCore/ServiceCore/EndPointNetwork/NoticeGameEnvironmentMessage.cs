using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NoticeGameEnvironmentMessage : IMessage
	{
		public int CafeType { get; set; }

		public bool IsOTP { get; set; }

		public NoticeGameEnvironmentMessage(int cafeType, bool isOTP)
		{
			this.CafeType = cafeType;
			this.IsOTP = isOTP;
		}

		public override string ToString()
		{
			return string.Format("NoticeGameEnvironmentMessage[ {0}cafe {1}otp ]", (this.CafeType == 1) ? "" : "Non-", this.IsOTP ? "" : "Non-");
		}
	}
}
