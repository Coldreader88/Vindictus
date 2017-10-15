using System;

namespace Devcat.Core.Threading.Profiler
{
	public class JobProfiler
	{
		public int CalculateIntervalMilliSecs { get; set; }

		public ValueType CalculatedValue { get; private set; }

		public DateTime CalculatedTime { get; private set; }

		public event EventHandler<EventArgs<Exception>> ExceptionOccur;

		public event EventHandler<EventArgs<ValueType>> ValueUpdated;

		public JobProfiler(IProfilePolicy policy)
		{
			this.policy = policy;
		}

		private void Calculate()
		{
			try
			{
				this.policy.Calculate();
				this.CalculatedValue = this.policy.LastValue;
				this.CalculatedTime = DateTime.Now;
				if (this.ValueUpdated != null)
				{
					this.ValueUpdated(this, new EventArgs<ValueType>(this.CalculatedValue));
				}
			}
			catch (Exception innerException)
			{
				if (this.ExceptionOccur != null)
				{
					try
					{
						this.ExceptionOccur(this, new EventArgs<Exception>(new Exception("Error while Calculate", innerException)));
					}
					catch (Exception)
					{
					}
				}
				this.policy.Flush();
			}
			if (this.CalculateIntervalMilliSecs > 0)
			{
				Scheduler.Schedule(this.thread, Job.Create(new Action(this.Calculate)), this.CalculateIntervalMilliSecs);
			}
		}

		public void Bind(JobProcessor thread)
		{
			if (this.thread != null)
			{
				this.Unbind();
			}
			if (thread == null)
			{
				return;
			}
			this.thread = thread;
			thread.Enqueued += this.thread_Enqueued;
			thread.Dequeued += this.thread_Dequeued;
			thread.Done += this.thread_Done;
			if (this.CalculateIntervalMilliSecs > 0)
			{
				Scheduler.Schedule(thread, Job.Create(new Action(this.Calculate)), this.CalculateIntervalMilliSecs);
			}
		}

		public void Unbind()
		{
			if (this.thread == null)
			{
				return;
			}
			this.thread.Enqueued -= this.thread_Enqueued;
			this.thread.Dequeued -= this.thread_Dequeued;
			this.thread.Done -= this.thread_Done;
			this.thread = null;
		}

		private void thread_Done(object sender, EventArgs<IJob> arg)
		{
			if (this.policy != null)
			{
				try
				{
					this.policy.JobDone(new JobProfileElement(arg.Value));
				}
				catch (Exception innerException)
				{
					if (this.ExceptionOccur != null)
					{
						try
						{
							this.ExceptionOccur(this, new EventArgs<Exception>(new Exception("Error while thread_Done", innerException)));
						}
						catch (Exception)
						{
						}
					}
				}
			}
		}

		private void thread_Dequeued(object sender, EventArgs<IJob> arg)
		{
		}

		private void thread_Enqueued(object sender, EventArgs<IJob> arg)
		{
			if (this.policy != null)
			{
				try
				{
					this.policy.JobEnqueue(new JobProfileElement(arg.Value));
				}
				catch (Exception innerException)
				{
					if (this.ExceptionOccur != null)
					{
						try
						{
							this.ExceptionOccur(this, new EventArgs<Exception>(new Exception("Error while thread_Enqueued", innerException)));
						}
						catch (Exception)
						{
						}
					}
				}
			}
		}

		private JobProcessor thread;

		private IProfilePolicy policy;
	}
}
