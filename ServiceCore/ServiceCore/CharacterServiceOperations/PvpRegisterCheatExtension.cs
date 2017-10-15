using System;

namespace ServiceCore.CharacterServiceOperations
{
	public static class PvpRegisterCheatExtension
	{
		public static bool Contains(this WhoIsOption value, WhoIsOption flag)
		{
			return (value & flag) == flag;
		}
	}
}
