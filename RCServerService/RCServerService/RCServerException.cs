using System;

namespace RemoteControlSystem.Server
{
	public class RCServerException : Exception
	{
		public RCServerException(string message) : base(message)
		{
		}

		public RCServerException(string message, params object[] args) : base(string.Format(message, args))
		{
		}
	}
}
