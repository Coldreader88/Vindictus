using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	public static class GPGainTypeUtil
	{
		public static bool IsValidGPGainTypeValue(int value, bool includeCheat)
		{
			return (includeCheat && value == 255) || (value >= 1 && value < 3);
		}

		public static bool IsValidGPGainTypeValue(GPGainType type, bool includeCheat)
		{
			return GPGainTypeUtil.IsValidGPGainTypeValue((int)type, includeCheat);
		}
	}
}
