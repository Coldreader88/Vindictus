using System;
using System.Diagnostics;
using System.Threading;
using Devcat.Core.Collections;
using Devcat.Core.ExceptionHandler;
using Devcat.Core.WinNative;

namespace Devcat.Core.Threading
{
	public class JobProcessor
	{
		public static JobProcessor Current
		{
			get
			{
				return JobProcessor.current;
			}
		}

		public IJob CurrentJob
		{
			get
			{
				return this.currentJob;
			}
		}

		public event EventHandler<EventArgs<Exception>> ExceptionFilter;

		public event EventHandler<EventArgs<Exception>> ExceptionOccur;

		public event EventHandler<EventArgs<IJob>> Enqueued;

		public event EventHandler<EventArgs<IJob>> Dequeued;

		public event EventHandler<EventArgs<IJob>> Done;

		public ThreadPriority ThreadPriority
		{
			get
			{
				return this.threadPriority;
			}
			set
			{
				this.threadPriority = value;
				if (this.thread != null)
				{
					this.thread.Priority = value;
				}
			}
		}

		public int JobCount
		{
			get
			{
				return this.jobCount - 1;
			}
		}

		public JobProcessor()
		{
			this.enqueueEvent = new AutoResetEvent(false);
			this.jobList = new WriteFreeQueue2<IJob>();
			this.jobCount = 1;
			this.status = JobProcessor.Status.Ready;
			this.threadPriority = ThreadPriority.Normal;
		}

		public void Start()
		{
			if (this.status == JobProcessor.Status.Ready)
			{
				this.thread = new System.Threading.Thread(new ThreadStart(this.Loop));
				this.thread.Priority = this.threadPriority;
				this.status = JobProcessor.Status.Running;
				this.thread.Start();
				return;
			}
			if (this.status == JobProcessor.Status.Running)
			{
				throw new InvalidOperationException("Already Started");
			}
			throw new InvalidOperationException("Already Closed");
		}

		public void Stop()
		{
			this.Stop(false);
		}

		public void Stop(bool doRemainJob)
		{
			if (doRemainJob)
			{
				this.status = JobProcessor.Status.Closing;
			}
			else
			{
				this.status = JobProcessor.Status.Closed;
			}
			this.enqueueEvent.Set();
		}

		public bool IsInThread()
		{
			return System.Threading.Thread.CurrentThread == this.thread;
		}

		public void Enqueue(IJob job)
		{
			if (this.status != JobProcessor.Status.Ready && this.status != JobProcessor.Status.Running)
			{
				return;
			}
			this.jobList.Enqueue(job);
			job.EnqueueTick = Stopwatch.GetTimestamp();
			if (this.Enqueued != null)
			{
				this.Enqueued(this, new EventArgs<IJob>(job));
			}
			if (Interlocked.Increment(ref this.jobCount) == 1)
			{
				this.enqueueEvent.Set();
			}
		}

		public void Join()
		{
			if (this.status == JobProcessor.Status.Ready)
			{
				throw new InvalidOperationException("Loop didn't started.");
			}
			if (this.IsInThread())
			{
				throw new InvalidOperationException("Can't join on current thread.");
			}
			if (this.status == JobProcessor.Status.Closed)
			{
				return;
			}
			this.thread.Join();
		}

		private void Loop()
		{
			JobProcessor.current = this;
			do
			{
				Util.Filter(delegate
				{
					while (this.status == JobProcessor.Status.Running)
					{
						if (this.jobCount == 1)
						{
							Devcat.Core.WinNative.Thread.SwitchToThread();
						}
						if (Interlocked.Decrement(ref this.jobCount) == 0)
						{
							this.enqueueEvent.WaitOne();
							if (this.status != JobProcessor.Status.Running)
							{
								break;
							}
						}
						IJob job = this.jobList.Dequeue();
						this.currentJob = job;
						job.StartTick = Stopwatch.GetTimestamp();
						if (this.Dequeued != null)
						{
							this.Dequeued(this, new EventArgs<IJob>(job));
						}
						job.Do();
						job.EndTick = Stopwatch.GetTimestamp();
						this.currentJob = null;
						if (this.Done != null)
						{
							this.Done(this, new EventArgs<IJob>(job));
						}
					}
					if (this.status == JobProcessor.Status.Closing)
					{
						while (!this.jobList.Empty)
						{
							IJob job2 = this.jobList.Dequeue();
							this.currentJob = job2;
							job2.StartTick = Stopwatch.GetTimestamp();
							if (this.Dequeued != null)
							{
								this.Dequeued(this, new EventArgs<IJob>(job2));
							}
							job2.Do();
							job2.EndTick = Stopwatch.GetTimestamp();
							this.currentJob = null;
							if (this.Done != null)
							{
								this.Done(this, new EventArgs<IJob>(job2));
							}
						}
						this.status = JobProcessor.Status.Closed;
					}
				}, delegate(Exception exception)
				{
					if (this.ExceptionFilter != null)
					{
						try
						{
							this.ExceptionFilter(this, new EventArgs<Exception>(exception));
						}
						catch
						{
						}
					}
				}, delegate(Exception exception)
				{
					if (this.ExceptionOccur != null)
					{
						try
						{
							this.ExceptionOccur(this, new EventArgs<Exception>(exception));
						}
						catch
						{
						}
					}
				});
			}
			while (this.status != JobProcessor.Status.Closed);
			this.jobList.Clear();
			this.enqueueEvent.Reset();
		}

		[ThreadStatic]
		private static JobProcessor current;

		private IJob currentJob;

		private JobProcessor.Status status;

		private System.Threading.Thread thread;

		private WriteFreeQueue2<IJob> jobList;

		private AutoResetEvent enqueueEvent;

		private int jobCount;

		private ThreadPriority threadPriority;

		private enum Status
		{
			Ready,
			Running,
			Closing,
			Closed
		}
	}
}
