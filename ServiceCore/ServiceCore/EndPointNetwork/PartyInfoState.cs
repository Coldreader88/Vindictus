using System;

namespace ServiceCore.EndPointNetwork
{
	public enum PartyInfoState : byte
	{
		InTown,
		InTownWithShipInfo,
		EnteringShip,
		InShip,
		Starting,
		InGame
	}
}
