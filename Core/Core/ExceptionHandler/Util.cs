using System;

namespace Devcat.Core.ExceptionHandler
{
	public class Util
	{
		private static bool DoFilter(Exception ex, Action<Exception> filter)
		{
			filter(ex);
			return true;
		}

		public static void Filter(Action body, Action<Exception> filter)
		{
			try
			{
				body();
			}
			catch (Exception ex) when (Util.DoFilter(ex, filter))
			{
			}
		}

		public static void Filter(Action body, Action<Exception> filter, Action<Exception> handler)
		{
			try
			{
				body();
			}
			catch (Exception ex) when (Util.DoFilter(ex, filter))
			{
				if (handler != null)
				{
					handler(ex);
				}
			}
		}
	}
}
