using System;

namespace ServiceCore
{
	[Flags]
	public enum Permission
	{
		Read = 1,
		Write = 2
	}
}
