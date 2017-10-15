using System;

namespace UnifiedNetwork.Cooperation
{
	public class OperationPerformanceDigest
	{
		public int OperationCount { get; set; }

		public long TotalSpendMilliseconds { get; set; }

		public long TotalStepCount { get; set; }

		public long TotalCoverageMilliseconds { get; set; }
	}
}
