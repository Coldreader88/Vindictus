using System;

namespace Nexon.Com.UserWrapper
{
	public class UserXMLParseException : Exception
	{
		public UserXMLParseException(string strErrorMessage, Exception ex) : base(strErrorMessage, ex)
		{
		}
	}
}
