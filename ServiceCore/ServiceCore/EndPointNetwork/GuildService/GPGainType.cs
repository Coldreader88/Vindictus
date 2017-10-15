using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	public enum GPGainType : byte
	{
		MIN_GP_GAIN_TYPE = 1,
		ByPVE = 1,
		ByPVP,
		MAX_GP_GAIN_TYPE,
		ByCheat = 255
	}
}
