using System;
using System.Collections.Generic;
using Utility;

namespace UnifiedNetwork.Cooperation
{
	public static class OperationPerformance
	{
		public static bool GatherPerformance { get; set; }

		public static void AddProbe(Type operationType, OperationProbe probe)
		{
			if (OperationPerformance.performances == null)
			{
				OperationPerformance.performances = new Dictionary<Type, OperationPerformanceDigest>();
			}
			if (!OperationPerformance.performances.ContainsKey(operationType))
			{
				OperationPerformance.performances[operationType] = new OperationPerformanceDigest();
			}
			OperationPerformanceDigest operationPerformanceDigest = OperationPerformance.performances[operationType];
			try
			{
				operationPerformanceDigest.OperationCount++;
				operationPerformanceDigest.TotalSpendMilliseconds += probe.SpendMilliseconds;
				operationPerformanceDigest.TotalStepCount += (long)probe.StepCount;
				operationPerformanceDigest.TotalCoverageMilliseconds += probe.CoverageMilliseconds;
			}
			catch (OverflowException ex)
			{
				Log<OperationPerformanceDigest>.Logger.Error("Operation digest overflow : ", ex);
				OperationPerformance.performances[operationType] = new OperationPerformanceDigest();
			}
			catch (Exception ex2)
			{
				Log<OperationPerformanceDigest>.Logger.Error("Operation digest exception : ", ex2);
			}
		}

		public static OperationPerformanceDigest GetDigest(Type operationType)
		{
			OperationPerformanceDigest result;
			OperationPerformance.performances.TryGetValue(operationType, out result);
			return result;
		}

		public static IEnumerable<KeyValuePair<Type, OperationPerformanceDigest>> GetAllDigests()
		{
			return OperationPerformance.performances;
		}

		public static void Reset()
		{
			OperationPerformance.performances.Clear();
		}

		[ThreadStatic]
		private static Dictionary<Type, OperationPerformanceDigest> performances;
	}
}
