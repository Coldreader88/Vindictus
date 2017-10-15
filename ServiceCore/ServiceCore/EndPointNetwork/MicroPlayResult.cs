using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public enum MicroPlayResult : byte
	{
		Success,
		SwearViolation,
		TimeOver,
		GiveUpQuest,
		HostDown,
		EmptyPlay,
		Exception
	}
}
