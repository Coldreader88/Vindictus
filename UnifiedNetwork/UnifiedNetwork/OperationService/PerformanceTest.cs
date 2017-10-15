using System;
using System.Collections.Generic;
using System.Linq;
using Devcat.Core.Threading;
using UnifiedNetwork.PipedNetwork;
using UnifiedNetwork.ProfileService;
using Utility;

namespace UnifiedNetwork.OperationService
{
	public class PerformanceTest
	{
		public PerformanceTest(Service s)
		{
			this.service = s;
			this.thread = s.Thread;
			this.RegisterPerformanceTestJob();
		}

		public void RegisterPerformanceTestJob()
		{
			this.moniter = new SystemMonitor();
			this.queueSizeHistory = new List<int>();
			this.cpuHistory = new List<double>();
			this.memoryHistory = new List<double>();
			Scheduler.Schedule(this.thread, Job.Create(new Action(this.DoPerformanceTestJob)), 1000);
		}

		private void DoPerformanceTestJob()
		{
			Log<Service>.Logger.WarnFormat("SERVER_STATUS[{0}] {1}:{2} JobQueueSize={3}, CPU={4} MEMORY={5} Peers={6}", new object[]
			{
				DateTime.Now.ToString("s"),
				this.service.Category,
				this.service.ID,
				this.thread.JobCount,
				this.moniter.CpuUse,
				this.moniter.AvailablePhysicalMemory,
				this.service.Peers.Count<Peer>()
			});
			Scheduler.Schedule(this.thread, Job.Create(new Action(this.DoPerformanceTestJob)), 1000);
		}

		private List<int> queueSizeHistory;

		private List<double> cpuHistory;

		private List<double> memoryHistory;

		private SystemMonitor moniter;

		private Service service;

		private JobProcessor thread;
	}
}
