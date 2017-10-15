using System;

namespace ServiceCore.EndPointNetwork
{
	public enum ReadyState : byte
	{
		InTown,
		InShip,
		Ready,
		Host,
		AssistWait,
		AssistAccepted,
		Rejoin
	}
}
