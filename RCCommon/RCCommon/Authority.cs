using System;

namespace RemoteControlSystem
{
	public enum Authority
	{
		None,
		UserWatcher,
		LogWatcher = 3,
		UserMonitor,
		GSM,
		UserKicker = 8,
		GM = 10,
		ChiefGM = 15,
		Developer = 20,
		Supervisor = 30,
		Operator = 35,
		Boss = 40,
		Root = 50
	}
}
