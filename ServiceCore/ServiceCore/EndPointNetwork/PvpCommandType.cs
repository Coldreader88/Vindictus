using System;

namespace ServiceCore.EndPointNetwork
{
	public enum PvpCommandType
	{
		StartGame,
		AbortGame,
		EndGame,
		AllPlayerReady,
		StartRound,
		EndRound,
		RegisteredPlayerQueue,
		RegisteredPlayerGame,
		UnregisteredPlayerQueue,
		UnregisteredPlayerGame,
		StateChanged,
		ArenaStartingPlayerInfo,
		ArenaEndGame
	}
}
