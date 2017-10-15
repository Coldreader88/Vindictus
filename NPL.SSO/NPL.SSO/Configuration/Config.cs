using System;
using System.Configuration;

namespace NPL.SSO.Configuration
{
	public static class Config
	{
		public static AuthenticatorSection Authenticator
		{
			get
			{
				return ConfigurationManager.GetSection("npl.sso/authenticator") as AuthenticatorSection;
			}
		}
	}
}
