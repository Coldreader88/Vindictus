using System;

namespace Nexon.CafeAuth.Packets
{
	public enum PolicyInfoErrorCode
	{
		Success,
		CannotFindAgeInfo,
		CannotAutholizing,
		BlockFromShutdownSystem,
		BlockFromSelectiveShutdownSystem,
		CannotPlayGame = 99,
		UnknownErrorCode
	}
}
