using System;

namespace Nexon.Com.UserWrapper
{
	public class UserWrapperException : Exception
	{
		public UserWrapperException(ErrorCode errorCode, string strErrorMessage, Exception ex) : base(strErrorMessage, ex)
		{
			this.ErrorCode = errorCode;
		}

		public ErrorCode ErrorCode { get; internal set; }
	}
}
