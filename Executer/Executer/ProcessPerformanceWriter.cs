using System;
using System.Diagnostics;
using System.Threading;
using Devcat.Core;
using Devcat.Core.Threading;
using Utility;

namespace Executer
{
	public class ProcessPerformanceWriter
	{
		public ProcessPerformanceWriter(IQueryPerformance[] perfArray, int periodMilis)
		{
			this.PerformanceArray = perfArray;
			this.PerformanceSchedulePeriodMillis = periodMilis;
		}

		public void Initialize()
		{
			this.process = Process.GetCurrentProcess();
			this.perfThread = new JobProcessor();
			this.perfThread.ExceptionOccur += delegate(object sender, EventArgs<Exception> e)
			{
				Console.WriteLine("Error at PerformanceThread: {0}", e.Value.ToString());
			};
			this.perfThread.Start();
			Scheduler.Schedule(this.perfThread, Job.Create(new Action(this.QueryPerformanceJob)), this.PerformanceSchedulePeriodMillis);
		}

		private float GetCpuUsagePercent(long ProcessorTicks)
		{
			long ticks = this.process.TotalProcessorTime.Ticks;
			if (DateTime.Now.Ticks == this.oldCpuCheckTime)
			{
				Thread.Sleep(1000);
			}
			long ticks2 = DateTime.Now.Ticks;
			long num = ticks2 - this.oldCpuCheckTime;
			long num2 = ticks - this.oldCPUUsage;
			float result = 0f;
			try
			{
				result = (float)num2 / (float)num / (float)Environment.ProcessorCount * 100f;
			}
			catch (OverflowException)
			{
				result = 0f;
			}
			this.oldCPUUsage = ticks;
			this.oldCpuCheckTime = ticks2;
			return result;
		}

		private void QueryPerformanceJob()
		{
			try
			{
				if (this.process != null && this.PerformanceArray != null && this.PerformanceArray.Length > 0)
				{
					this.process.Refresh();
					long privateMemorySize = this.process.PrivateMemorySize64;
					long virtualMemorySize = this.process.VirtualMemorySize64;
					long num = (long)this.GetCpuUsagePercent(this.process.TotalProcessorTime.Ticks);
					long num2 = 0L;
					long num3 = 0L;
					foreach (IQueryPerformance queryPerformance in this.PerformanceArray)
					{
						if (queryPerformance == null)
						{
							Console.WriteLine("Error element is null");
						}
						else
						{
							num2 += queryPerformance.GetEntityCount();
							num3 += queryPerformance.GetQueueLength();
						}
					}
					string value = string.Format("QueryPerf {0} {1} {2} {3} {4}", new object[]
					{
						num,
						privateMemorySize,
						virtualMemorySize,
						num2,
						num3
					});
					Console.WriteLine(value);
				}
				else
				{
					Console.WriteLine("Null Exception occured while processing Performance.");
				}
				Scheduler.Schedule(this.perfThread, Job.Create(new Action(this.QueryPerformanceJob)), this.PerformanceSchedulePeriodMillis);
			}
			catch (Exception ex)
			{
				Console.WriteLine("End of PerformanceWriting: " + ex.ToString());
			}
		}

		private IQueryPerformance[] PerformanceArray;

		private JobProcessor perfThread;

		private Process process;

		private int PerformanceSchedulePeriodMillis = 5000;

		private long oldCPUUsage;

		private long oldCpuCheckTime;
	}
}
