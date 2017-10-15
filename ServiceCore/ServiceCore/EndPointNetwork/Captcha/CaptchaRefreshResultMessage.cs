using System;

namespace ServiceCore.EndPointNetwork.Captcha
{
	[Serializable]
	public class CaptchaRefreshResultMessage : IMessage
	{
		public CaptchaRefreshResultMessage(CaptchaRefreshResult ErrorCode)
		{
			this.ErrorCode = (int)ErrorCode;
		}

		private int ErrorCode;
	}
}
