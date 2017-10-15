using System;

namespace Nexon.CafeAuthJPN
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
