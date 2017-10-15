using System;

namespace ServiceCore.EndPointNetwork.Captcha
{
	[Serializable]
	public sealed class CaptchaResponseResultMessage : IMessage
	{
		public CaptchaResponseResultMessage(CaptchaResponseResult Result)
		{
			this.Result = (int)Result;
		}

		private int Result;
	}
}
