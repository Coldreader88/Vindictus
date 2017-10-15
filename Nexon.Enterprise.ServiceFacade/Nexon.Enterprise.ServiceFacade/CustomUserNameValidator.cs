using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace Nexon.Enterprise.ServiceFacade
{
	public class CustomUserNameValidator : UserNamePasswordValidator
	{
		public override void Validate(string userName, string password)
		{
			if (userName == null || password == null)
			{
				throw new ArgumentNullException();
			}
			if (!userName.Equals(password, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Unknown Username or Password");
			}
		}
	}
}
