using System;

namespace Devcat.Core.Threading.Profiler
{
	public struct JobProfileElement
	{
		public long EnqueueTick { get; set; }

		public long StartTick { get; set; }

		public long EndTick { get; set; }

		public long GetWaitTicks()
		{
			if (this.StartTick == 0L || this.EnqueueTick == 0L || this.StartTick < this.EnqueueTick)
			{
				return 0L;
			}
			return this.StartTick - this.EnqueueTick;
		}

		public long GetProcessTicks()
		{
			if (this.EndTick == 0L || this.StartTick == 0L || this.EndTick < this.StartTick)
			{
				return 0L;
			}
			return this.EndTick - this.StartTick;
		}

		public long GetDoneTicks()
		{
			if (this.EndTick == 0L || this.EnqueueTick == 0L || this.EndTick < this.EnqueueTick)
			{
				return 0L;
			}
			return this.EndTick - this.EnqueueTick;
		}

		public JobProfileElement(IJob job)
		{
			this = default(JobProfileElement);
			this.EnqueueTick = job.EnqueueTick;
			this.StartTick = job.StartTick;
			this.EndTick = job.EndTick;
		}

		public override string ToString()
		{
			return string.Format("JobProfileElement[ EnqueueTick {0} | StartTick {1} | EndTick {2} ]", this.EnqueueTick, this.StartTick, this.EndTick);
		}
	}
}
