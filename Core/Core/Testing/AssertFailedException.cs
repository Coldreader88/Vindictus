using System;

namespace Devcat.Core.Testing
{
	public class AssertFailedException : TestFailedException
	{
		public AssertFailedException(string message) : base(message)
		{
		}
	}
}
