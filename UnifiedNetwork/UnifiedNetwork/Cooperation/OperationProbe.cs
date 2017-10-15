using System;
using System.Collections.Generic;
using System.Linq;

namespace UnifiedNetwork.Cooperation
{
	public struct OperationProbe
	{
		internal void AddStep(StepProbe probe)
		{
			if (this.steps == null)
			{
				this.steps = new List<StepProbe>();
			}
			this.steps.Add(probe);
		}

		public long CoverageMilliseconds
		{
			get
			{
				if (this.steps == null || this.steps.Count <= 0)
				{
					return 0L;
				}
				StepProbe stepProbe = this.steps[0];
				return (long)this.steps[this.steps.Count - 1].EndTime.Subtract(stepProbe.StartTime).TotalMilliseconds;
			}
		}

		public long SpendMilliseconds
		{
			get
			{
				if (this.steps == null || this.steps.Count <= 0)
				{
					return 0L;
				}
				return this.steps.Aggregate(0L, (long total, StepProbe step) => total + step.SpendMilliseconds);
			}
		}

		public int StepCount
		{
			get
			{
				if (this.steps == null || this.steps.Count <= 0)
				{
					return 0;
				}
				return this.steps.Count;
			}
		}

		private List<StepProbe> steps;
	}
}
