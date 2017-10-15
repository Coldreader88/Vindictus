using System;
using System.Threading;

namespace Nexon.Enterprise.ServiceFacade
{
	public struct TimeoutHelper
	{
		public static TimeSpan DefaultTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(2.0);
			}
		}

		public static TimeSpan DefaultShortTimeout
		{
			get
			{
				return TimeSpan.FromSeconds(4.0);
			}
		}

		public static TimeSpan Infinite
		{
			get
			{
				return TimeSpan.MaxValue;
			}
		}

		public TimeoutHelper(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			this.originalTimeout = timeout;
			if (timeout == TimeSpan.MaxValue)
			{
				this.deadline = DateTime.MaxValue;
				return;
			}
			this.deadline = DateTime.UtcNow + timeout;
		}

		public TimeSpan OriginalTimeout
		{
			get
			{
				return this.originalTimeout;
			}
		}

		public TimeSpan RemainingTime()
		{
			if (this.deadline == DateTime.MaxValue)
			{
				return TimeSpan.MaxValue;
			}
			TimeSpan timeSpan = this.deadline - DateTime.UtcNow;
			if (timeSpan <= TimeSpan.Zero)
			{
				return TimeSpan.Zero;
			}
			return timeSpan;
		}

		public void SetTimer(TimerCallback callback, object state)
		{
			new Timer(callback, state, TimeoutHelper.ToMilliseconds(this.RemainingTime()), -1);
		}

		public static TimeSpan FromMilliseconds(int milliseconds)
		{
			if (milliseconds == -1)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromMilliseconds((double)milliseconds);
		}

		public static TimeSpan FromMilliseconds(uint milliseconds)
		{
			if (milliseconds == 4294967295u)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromMilliseconds(milliseconds);
		}

		public static int ToMilliseconds(TimeSpan timeout)
		{
			if (timeout == TimeSpan.MaxValue)
			{
				return -1;
			}
			long num = TimeoutHelper.Ticks.FromTimeSpan(timeout);
			if (num / 10000L > 2147483647L)
			{
				return int.MaxValue;
			}
			return TimeoutHelper.Ticks.ToMilliseconds(num);
		}

		public static TimeSpan Add(TimeSpan timeout1, TimeSpan timeout2)
		{
			return TimeoutHelper.Ticks.ToTimeSpan(TimeoutHelper.Ticks.Add(TimeoutHelper.Ticks.FromTimeSpan(timeout1), TimeoutHelper.Ticks.FromTimeSpan(timeout2)));
		}

		public static DateTime Add(DateTime time, TimeSpan timeout)
		{
			if (timeout >= TimeSpan.Zero && DateTime.MaxValue - time <= timeout)
			{
				return DateTime.MaxValue;
			}
			if (timeout <= TimeSpan.Zero && DateTime.MinValue - time >= timeout)
			{
				return DateTime.MinValue;
			}
			return time + timeout;
		}

		public static DateTime Subtract(DateTime time, TimeSpan timeout)
		{
			return TimeoutHelper.Add(time, TimeSpan.Zero - timeout);
		}

		public static TimeSpan Divide(TimeSpan timeout, int factor)
		{
			if (timeout == TimeSpan.MaxValue)
			{
				return TimeSpan.MaxValue;
			}
			return TimeoutHelper.Ticks.ToTimeSpan(TimeoutHelper.Ticks.FromTimeSpan(timeout) / (long)factor + 1L);
		}

		public static bool WaitOne(WaitHandle waitHandle, TimeSpan timeout, bool exitSync)
		{
			bool result;
			try
			{
				if (timeout == TimeSpan.MaxValue)
				{
					waitHandle.WaitOne();
					result = true;
				}
				else
				{
					TimeSpan timeSpan = TimeSpan.FromMilliseconds(2147483647.0);
					while (timeout > timeSpan)
					{
						bool flag = waitHandle.WaitOne(timeSpan, exitSync);
						if (flag)
						{
							return true;
						}
						timeout -= timeSpan;
					}
					result = waitHandle.WaitOne(timeout, exitSync);
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		private DateTime deadline;

		private TimeSpan originalTimeout;

		private static class Ticks
		{
			public static long FromMilliseconds(int milliseconds)
			{
				return (long)milliseconds * 10000L;
			}

			public static int ToMilliseconds(long ticks)
			{
				return checked((int)(ticks / 10000L));
			}

			public static long FromTimeSpan(TimeSpan duration)
			{
				return duration.Ticks;
			}

			public static TimeSpan ToTimeSpan(long ticks)
			{
				return new TimeSpan(ticks);
			}

			public static long Add(long firstTicks, long secondTicks)
			{
				if (firstTicks == 9223372036854775807L || firstTicks == -9223372036854775808L)
				{
					return firstTicks;
				}
				if (secondTicks == 9223372036854775807L || secondTicks == -9223372036854775808L)
				{
					return secondTicks;
				}
				if (firstTicks >= 0L && 9223372036854775807L - firstTicks <= secondTicks)
				{
					return 9223372036854775806L;
				}
				if (firstTicks <= 0L && -9223372036854775808L - firstTicks >= secondTicks)
				{
					return -9223372036854775807L;
				}
				return checked(firstTicks + secondTicks);
			}
		}
	}
}
