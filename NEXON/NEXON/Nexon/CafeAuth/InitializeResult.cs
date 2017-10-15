using System;

namespace Nexon.CafeAuth
{
	public enum InitializeResult : byte
	{
		Success,
		Duplicated,
		Illegal,
		Error = 99
	}
}
