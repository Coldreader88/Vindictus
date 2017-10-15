using System;

namespace Nexon.CafeAuthOld
{
	public enum Result : byte
	{
		Forbidden,
		Allowed,
		Trial,
		Terminate,
		Message
	}
}
