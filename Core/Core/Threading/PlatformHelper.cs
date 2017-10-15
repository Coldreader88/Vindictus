using System;

namespace Devcat.Core.Threading
{
	internal static class PlatformHelper
	{
		internal static bool IsSingleProcessor
		{
			get
			{
				return PlatformHelper.ProcessorCount == 1;
			}
		}

		internal static int ProcessorCount
		{
			get
			{
				if (DateTime.UtcNow.CompareTo(PlatformHelper.s_nextProcessorCountRefreshTime) >= 0)
				{
					PlatformHelper.s_processorCount = Environment.ProcessorCount;
					PlatformHelper.s_nextProcessorCountRefreshTime = DateTime.UtcNow.AddMilliseconds(30000.0);
				}
				return PlatformHelper.s_processorCount;
			}
		}

		private const int PROCESSOR_COUNT_REFRESH_INTERVAL_MS = 30000;

		private static DateTime s_nextProcessorCountRefreshTime = DateTime.MinValue;

		private static int s_processorCount = -1;
	}
}
