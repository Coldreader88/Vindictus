using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Devcat.Core.Threading.Profiler
{
	public class AverageProfilePolicy : IProfilePolicy
	{
		public ValueType LastValue { get; private set; }

		public event EventHandler<EventArgs<ValueType>> Calculated;

		public void JobEnqueue(JobProfileElement element)
		{
		}

		public void JobDone(JobProfileElement element)
		{
			this.elements.Add(element);
		}

		public void Calculate()
		{
			this.LastValue = new ProfileIndex<double>
			{
				WaitTimeIndex = this.WaitTimeIndex,
				ProcessTimeIndex = this.ProcessTimeIndex,
				DoneTimeIndex = this.DoneTimeIndex,
				SampleCount = this.elements.Count
			};
			this.Flush();
			if (this.Calculated != null)
			{
				this.Calculated(this, new EventArgs<ValueType>(this.LastValue));
			}
		}

		public double WaitTimeIndex
		{
			get
			{
				if (this.elements.Count <= 0)
				{
					return 0.0;
				}
				return this.elements.Average((JobProfileElement x) => (double)x.GetWaitTicks()) / (double)Stopwatch.Frequency;
			}
		}

		public double ProcessTimeIndex
		{
			get
			{
				if (this.elements.Count <= 0)
				{
					return 0.0;
				}
				return this.elements.Average((JobProfileElement x) => (double)x.GetProcessTicks()) / (double)Stopwatch.Frequency;
			}
		}

		public double DoneTimeIndex
		{
			get
			{
				if (this.elements.Count <= 0)
				{
					return 0.0;
				}
				return this.elements.Average((JobProfileElement x) => (double)x.GetDoneTicks()) / (double)Stopwatch.Frequency;
			}
		}

		public void Flush()
		{
			this.elements.Clear();
		}

		private List<JobProfileElement> elements = new List<JobProfileElement>();
	}
}
