using System;

namespace ServiceCore.EndPointNetwork
{
	[Flags]
	public enum PvpRegisterCheat
	{
		None = 0,
		IgnoreRestriction = 1,
		ForceStart = 2,
		Test = 4,
		Debug = 8
	}
}
