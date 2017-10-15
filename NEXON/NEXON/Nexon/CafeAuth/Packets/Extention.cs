using System;

namespace Nexon.CafeAuth.Packets
{
	public static class Extention
	{
		public static PolicyInfoErrorCode ToErrorCode(this int value)
		{
			switch (value)
			{
			case 0:
				return PolicyInfoErrorCode.Success;
			case 1:
				return PolicyInfoErrorCode.CannotFindAgeInfo;
			case 2:
				return PolicyInfoErrorCode.CannotAutholizing;
			case 3:
				return PolicyInfoErrorCode.BlockFromShutdownSystem;
			case 4:
				return PolicyInfoErrorCode.BlockFromSelectiveShutdownSystem;
			default:
				if (value != 99)
				{
					return PolicyInfoErrorCode.UnknownErrorCode;
				}
				return PolicyInfoErrorCode.CannotPlayGame;
			}
		}
	}
}
