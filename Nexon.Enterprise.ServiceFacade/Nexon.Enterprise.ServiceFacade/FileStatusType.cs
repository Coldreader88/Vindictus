using System;

namespace Nexon.Enterprise.ServiceFacade
{
	[Flags]
	public enum FileStatusType
	{
		None = 0,
		Changed = 1,
		Deleted = 4,
		All = 2147483647
	}
}
