using System;

namespace Devcat.Core.Testing
{
	public class TestFailedException : Exception
	{
		public TestFailedException()
		{
		}

		public TestFailedException(string message) : base(message)
		{
		}

		public TestFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
