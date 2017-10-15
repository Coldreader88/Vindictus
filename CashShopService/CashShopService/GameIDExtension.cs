using System;

namespace CashShopService
{
	public static class GameIDExtension
	{
		public static long? GetCID(this string gameID)
		{
			if (!gameID.StartsWith("Char"))
			{
				return null;
			}
			long value;
			if (long.TryParse(gameID.Substring(4), out value))
			{
				return new long?(value);
			}
			return null;
		}
	}
}
