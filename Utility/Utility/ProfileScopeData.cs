using System;

namespace Utility
{
	public class ProfileScopeData
	{
		public string Tag
		{
			get
			{
				return this.tag;
			}
		}

		public double MinDuration
		{
			get
			{
				if (this.minDuration == null)
				{
					return 0.0;
				}
				return this.minDuration.Value;
			}
		}

		public double MaxDuration
		{
			get
			{
				if (this.maxDuration == null)
				{
					return 0.0;
				}
				return this.maxDuration.Value;
			}
		}

		public double AvgDuration
		{
			get
			{
				if (this.totalCallCount <= 0L)
				{
					return 0.0;
				}
				return this.totalDuration / (double)this.totalCallCount;
			}
		}

		public double TotalDuration
		{
			get
			{
				return this.totalDuration;
			}
		}

		public long TotalCallCount
		{
			get
			{
				return this.totalCallCount;
			}
		}

		internal ProfileScopeData(string tag)
		{
			this.tag = tag;
			this.minDuration = null;
			this.maxDuration = null;
			this.totalDuration = 0.0;
			this.totalCallCount = 0L;
		}

		public void Update(double duration)
		{
			if (duration >= 0.0)
			{
				if (this.minDuration == null || duration < this.minDuration.Value)
				{
					this.minDuration = new double?(duration);
				}
				if (this.maxDuration == null || duration > this.maxDuration.Value)
				{
					this.maxDuration = new double?(duration);
				}
				this.totalDuration += duration;
			}
			this.totalCallCount += 1L;
		}

		private string tag;

		private double? minDuration;

		private double? maxDuration;

		private double totalDuration;

		private long totalCallCount;
	}
}
