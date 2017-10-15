using System;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	[Flags]
	public enum ChunkingAppliesTo
	{
		InMessage = 1,
		OutMessage = 2,
		Both = 3
	}
}
