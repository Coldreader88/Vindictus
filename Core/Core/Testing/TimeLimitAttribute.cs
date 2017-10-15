using System;

namespace Devcat.Core.Testing
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public class TimeLimitAttribute : Attribute
	{
		public TimeSpan TimeLimit
		{
			get
			{
				return this.timeLimit;
			}
			set
			{
				this.timeLimit = value;
			}
		}

		public TimeLimitAttribute(TimeSpan timeLimit)
		{
			this.timeLimit = timeLimit;
		}

		private TimeSpan timeLimit;
	}
}
