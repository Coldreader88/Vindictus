using System;

namespace ServiceCore.EndPointNetwork
{
	public static class TitleStateExtension
	{
		public static bool Has(this TitleState state)
		{
			return state != TitleState.Unknown;
		}

		public static bool IsKnown(this TitleState state)
		{
			return state == TitleState.Known || state == TitleState.Acquired;
		}

		public static bool IsAcquired(this TitleState state)
		{
			return state == TitleState.Acquired;
		}
	}
}
