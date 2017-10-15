using System;
using System.Collections.Generic;
using System.Threading;

namespace Devcat.Core.Threading
{
	public static class Scheduler
	{
		public static event EventHandler<EventArgs<Exception>> ExceptionOccur;

		static Scheduler()
		{
			Scheduler.enqueueEvent = new ManualResetEvent(false);
			Scheduler.scheduleList = new SortedDictionary<DateTime, Scheduler.JobPair>();
			Scheduler.thread = new Thread(new ThreadStart(Scheduler.Loop));
			Scheduler.thread.IsBackground = true;
			Scheduler.thread.Start();
			AppDomain.CurrentDomain.DomainUnload += Scheduler.Abort;
			AppDomain.CurrentDomain.ProcessExit += Scheduler.Abort;
		}

		private static void Abort(object sender, EventArgs e)
		{
			Scheduler.domainUnloading = true;
			Scheduler.enqueueEvent.Set();
		}

		public static long Schedule(JobProcessor loop, IJob job, int milliSecond)
		{
			return Scheduler.Schedule(loop, job, DateTime.UtcNow.AddTicks((long)milliSecond * 10000L));
		}

		public static long Schedule(JobProcessor loop, IJob job, TimeSpan timeSpan)
		{
			return Scheduler.Schedule(loop, job, DateTime.UtcNow + timeSpan);
		}

		private static long Schedule(JobProcessor loop, IJob job, DateTime time)
		{
			lock (Scheduler.scheduleList)
			{
				while (Scheduler.scheduleList.ContainsKey(time))
				{
					time = time.AddTicks(1L);
				}
				Scheduler.scheduleList.Add(time, new Scheduler.JobPair(loop, job));
			}
			Scheduler.enqueueEvent.Set();
			return time.Ticks;
		}

		public static bool Cancel(long scheduleID)
		{
			lock (Scheduler.scheduleList)
			{
				DateTime key = new DateTime(scheduleID);
				if (Scheduler.scheduleList.ContainsKey(key))
				{
					Scheduler.scheduleList.Remove(key);
					return true;
				}
			}
			return false;
		}

        private static void Loop()
        {
            Scheduler.JobPair jobPair;
            int nextDelay = 0;
            while (!Scheduler.domainUnloading)
            {
                Scheduler.enqueueEvent.WaitOne(nextDelay, false);
                DateTime utcNow = DateTime.UtcNow;
                lock (Scheduler.scheduleList)
                {
                    nextDelay = Scheduler.GetNextDelay(out jobPair);
                    if (nextDelay == 0)
                    {
                        Scheduler.RemoveFirstScheduled();
                        nextDelay = Scheduler.GetNextDelay();
                    }
                    else
                    {
                        Scheduler.enqueueEvent.Reset();
                        continue;
                    }
                }
                if (jobPair.JobProcessor != null)
                {
                    jobPair.JobProcessor.Enqueue(jobPair.Job);
                }
                else
                {
                    try
                    {
                        jobPair.Job.Do();
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        try
                        {
                            if (Scheduler.ExceptionOccur != null)
                            {
                                Scheduler.ExceptionOccur(null, new EventArgs<Exception>(exception));
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                jobPair.Job = null;
            }
        }

        private static int GetNextDelay()
		{
			Scheduler.JobPair jobPair;
			return Scheduler.GetNextDelay(out jobPair);
		}

		private static int GetNextDelay(out Scheduler.JobPair jobPair)
		{
			jobPair = default(Scheduler.JobPair);
			using (SortedDictionary<DateTime, Scheduler.JobPair>.Enumerator enumerator = Scheduler.scheduleList.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<DateTime, Scheduler.JobPair> keyValuePair = enumerator.Current;
					jobPair = keyValuePair.Value;
					long num = (keyValuePair.Key.Ticks - DateTime.UtcNow.Ticks) / 10000L;
					if (num < 0L)
					{
						return 0;
					}
					if (num > 2147483647L)
					{
						return int.MaxValue;
					}
					return (int)num;
				}
			}
			return -1;
		}

		private static void RemoveFirstScheduled()
		{
			using (SortedDictionary<DateTime, Scheduler.JobPair>.Enumerator enumerator = Scheduler.scheduleList.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<DateTime, Scheduler.JobPair> keyValuePair = enumerator.Current;
					Scheduler.scheduleList.Remove(keyValuePair.Key);
				}
			}
		}

		private static Thread thread;

		private static SortedDictionary<DateTime, Scheduler.JobPair> scheduleList;

		private static ManualResetEvent enqueueEvent;

		private static bool domainUnloading = false;

		private struct JobPair
		{
			public JobPair(JobProcessor jobProcessor, IJob job)
			{
				this.JobProcessor = jobProcessor;
				this.Job = job;
			}

			public JobProcessor JobProcessor;

			public IJob Job;
		}
	}
}
