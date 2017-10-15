using System;

namespace Nexon.CafeAuth
{
	public enum AuthorizeResult : byte
	{
		Forbidden,
		Allowed,
		Trial,
		Terminate,
		Message
	}
}
