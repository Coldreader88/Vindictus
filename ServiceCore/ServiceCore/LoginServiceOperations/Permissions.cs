using System;

namespace ServiceCore.LoginServiceOperations
{
	[Flags]
	public enum Permissions
	{
		Login = 1,
		LoginWhileMaintenance = 2,
		AlreadyUsed2 = 4,
		NotUsed3 = 8,
		AlreadyUsed4 = 16,
		AlreadyUsed5 = 32,
		AlreadyUsed6 = 64,
		AlreadyUsed7 = 128,
		LoginAsOtherAccount = 8388608,
		LoginWithoutHackshield = 16777216,
		Cheat = 33554432,
		ShutdownServer = 67108864,
		Announce = 134217728,
		IgnoreTicketing = 268435456,
		ReloadScript = 536870912,
		ForceJoin = 1073741824,
		PermissionMax = -2147483648
	}
}
