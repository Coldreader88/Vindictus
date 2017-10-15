using System;
using System.Collections.Generic;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.OperationService;
using Utility;

namespace ServiceCore
{
	internal class PerformanceLogger
	{
		public PerformanceLogger(Service service, int interval)
		{
			this.service = service;
			this.interval = interval;
		}

		public void Start()
		{
			if (JobProcessor.Current != this.service.Thread)
			{
				this.service.Thread.Enqueue(Job.Create(new Action(this.Start)));
				return;
			}
			if (OperationPerformance.GatherPerformance)
			{
				return;
			}
			OperationPerformance.GatherPerformance = true;
			ScheduleRepeater.Schedule(this.service.Thread, Job.Create(new Action(this.PerformanceLog)), this.interval, false);
		}

		public void PerformanceLog()
		{
			using (PerformanceLogDataContext performanceLogDataContext = new PerformanceLogDataContext())
			{
				foreach (KeyValuePair<Type, OperationPerformanceDigest> keyValuePair in OperationPerformance.GetAllDigests())
				{
					performanceLogDataContext.LogOperationPerf(new long?((long)this.service.ID), this.service.Category, keyValuePair.Key.Name, new int?(keyValuePair.Value.OperationCount), new long?(keyValuePair.Value.TotalSpendMilliseconds), new long?(keyValuePair.Value.TotalStepCount), new long?(keyValuePair.Value.TotalCoverageMilliseconds), new int?(this.interval));
				}
				OperationPerformance.Reset();
			}
		}

		private Service service;

		private int interval;
	}
}
