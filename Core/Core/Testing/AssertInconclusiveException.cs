using System;

namespace Devcat.Core.Testing
{
	public class AssertInconclusiveException : TestFailedException
	{
		public AssertInconclusiveException() : base("이 테스트 케이스는 아직 구현되지 않았습니다.")
		{
		}

		public AssertInconclusiveException(string message) : base(message)
		{
		}
	}
}
