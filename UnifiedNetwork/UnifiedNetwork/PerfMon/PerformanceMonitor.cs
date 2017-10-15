using System;
using System.Diagnostics;
using Devcat.Core.Threading;
using Utility;

namespace UnifiedNetwork.PerfMon
{
	public class PerformanceMonitor
	{
		public float CpuPercentTotal { get; private set; }

		public float RawCpuPercentTotal
		{
			get
			{
				return this.cpuCounter.NextValue();
			}
		}

		public event EventHandler ValueUpdated;

		public PerformanceMonitor(JobProcessor thread, int interval)
		{
			this.CpuPercentTotal = this.RawCpuPercentTotal;
			this.thread = thread;
			this.interval = interval;
			if (thread != null && interval > 0)
			{
				Action timerFunc = null;
				timerFunc = delegate
				{
					this.UpdateValue();
					Scheduler.Schedule(thread, Job.Create(timerFunc), interval);
				};
				timerFunc();
			}
		}

		public void UpdateValue()
		{
			this.CpuPercentTotal = this.RawCpuPercentTotal;
			if (this.ValueUpdated != null)
			{
				try
				{
					this.ValueUpdated(this, null);
				}
				catch (Exception ex)
				{
					Log<PerformanceMonitor>.Logger.Error("Error while ValueUpdated : ", ex);
				}
			}
		}

		private PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

		private JobProcessor thread;

		private int interval;
	}
}
