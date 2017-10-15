using System;

namespace ServiceCore.EndPointNetwork
{
	public static class PvpRegisterCheatExtension
	{
		public static bool Contains(this PvpRegisterCheat value, PvpRegisterCheat flag)
		{
			return (value & flag) == flag;
		}
	}
}
