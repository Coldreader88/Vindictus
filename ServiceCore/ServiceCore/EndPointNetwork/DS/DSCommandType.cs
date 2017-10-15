using System;

namespace ServiceCore.EndPointNetwork.DS
{
	public enum DSCommandType
	{
		ClientCmd,
		StopDS,
		ServerStarted,
		StartFailTimer,
		BlockEntering,
		CheatOn,
		CheatOff,
		ReportFps
	}
}
