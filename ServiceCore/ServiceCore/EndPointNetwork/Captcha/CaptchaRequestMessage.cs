using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Captcha
{
	[Serializable]
	public sealed class CaptchaRequestMessage : IMessage
	{
		public int AuthCode { get; set; }

		public List<byte> Image { get; set; }

		public int Remain { get; set; }

		public bool Recaptcha { get; set; }
	}
}
