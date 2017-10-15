using System;

namespace ServiceCore.EndPointNetwork.Captcha
{
	[Serializable]
	public sealed class CaptchaResponseMessage : IMessage
	{
		public int AuthCode { get; set; }

		public string Response { get; set; }
	}
}
