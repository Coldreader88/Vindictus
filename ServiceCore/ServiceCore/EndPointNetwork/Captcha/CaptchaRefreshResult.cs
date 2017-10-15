using System;

namespace ServiceCore.EndPointNetwork.Captcha
{
	public enum CaptchaRefreshResult
	{
		Success,
		CountOver,
		Cooldown,
		LogicalFail
	}
}
