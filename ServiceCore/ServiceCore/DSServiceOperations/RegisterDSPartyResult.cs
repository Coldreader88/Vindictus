using System;

namespace ServiceCore.DSServiceOperations
{
	public enum RegisterDSPartyResult
	{
		Unknown,
		Success,
		NoSuchQuest,
		AlreadyInQueue,
		CannotJoinQuest,
		InvalidBossID
	}
}
