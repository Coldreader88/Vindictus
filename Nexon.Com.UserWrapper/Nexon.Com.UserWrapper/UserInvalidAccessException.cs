using System;

namespace Nexon.Com.UserWrapper
{
	public class UserInvalidAccessException : Exception
	{
		public UserInvalidAccessException() : base("Invalid Property Access", null)
		{
		}

		public UserInvalidAccessException(string strErrorMessage, Exception ex) : base(strErrorMessage, ex)
		{
		}
	}
}
