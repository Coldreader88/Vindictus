using System;

namespace UnifiedNetwork.Cooperation
{
	internal struct StepProbe
	{
		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public long SpendMilliseconds
		{
			get
			{
				return (long)this.EndTime.Subtract(this.StartTime).TotalMilliseconds;
			}
		}
	}
}
