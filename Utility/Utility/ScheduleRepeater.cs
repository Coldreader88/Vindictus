using System;
using Devcat.Core.Threading;

namespace Utility
{
	public class ScheduleRepeater
	{
		private void Repeat()
		{
			try
			{
				this.job.Do();
			}
			catch (Exception ex)
			{
				Log<ScheduleRepeater>.Logger.Error("Error while repeat job.Do : ", ex);
			}
			Scheduler.Schedule(this.thread, Job.Create(new Action(this.Repeat)), this.interval);
		}

		public static void Schedule(JobProcessor thread, IJob job, int interval, bool instant)
		{
			ScheduleRepeater @object = new ScheduleRepeater
			{
				thread = thread,
				job = job,
				interval = interval
			};
			if (instant)
			{
				thread.Enqueue(Job.Create(new Action(@object.Repeat)));
				return;
			}
			Scheduler.Schedule(thread, Job.Create(new Action(@object.Repeat)), interval);
		}

		private IJob job;

		private int interval;

		private JobProcessor thread;
	}
}
