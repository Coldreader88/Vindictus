using System;

namespace Nexon.Com.Group.Game.Wrapper
{
	public class GroupException : Exception
	{
		public GroupException(int ErrorCode, string ErrorMessage) : base(ErrorMessage)
		{
			this.ErrorCode = ErrorCode;
		}

		public int ErrorCode { get; private set; }
	}
}
