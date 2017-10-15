using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace Devcat.Core.Threading
{
	[DebuggerDisplay("Current Count = {m_currentCount}")]
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class SemaphoreSlim : IDisposable
	{
		public SemaphoreSlim(int initialCount) : this(initialCount, int.MaxValue)
		{
		}

		public SemaphoreSlim(int initialCount, int maxCount)
		{
			if (initialCount < 0 || initialCount > maxCount)
			{
				throw new ArgumentOutOfRangeException("initialCount", initialCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_ctor_InitialCountWrong"));
			}
			if (maxCount <= 0)
			{
				throw new ArgumentOutOfRangeException("maxCount", maxCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_ctor_MaxCountWrong"));
			}
			this.m_maxCount = maxCount;
			this.m_lockObj = new object();
			this.m_currentCount = initialCount;
		}

		private static void CancellationTokenCanceledEventHandler(object obj)
		{
			SemaphoreSlim semaphoreSlim = obj as SemaphoreSlim;
			lock (semaphoreSlim.m_lockObj)
			{
				Monitor.PulseAll(semaphoreSlim.m_lockObj);
			}
		}

		private void CheckDispose()
		{
			if (this.m_lockObj == null)
			{
				throw new ObjectDisposedException(null, SemaphoreSlim.GetResourceString("SemaphoreSlim_Disposed"));
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_waitHandle != null)
				{
					this.m_waitHandle.Close();
					this.m_waitHandle = null;
				}
				this.m_lockObj = null;
			}
		}

		private static string GetResourceString(string str)
		{
			return Environment2.GetResourceString(str);
		}

		public int Release()
		{
			return this.Release(1);
		}

		public int Release(int releaseCount)
		{
			this.CheckDispose();
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", releaseCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_Release_CountWrong"));
			}
			int result;
			lock (this.m_lockObj)
			{
				if (this.m_maxCount - this.m_currentCount < releaseCount)
				{
					throw new SemaphoreFullException();
				}
				this.m_currentCount += releaseCount;
				if (this.m_currentCount == 1 || this.m_waitCount == 1)
				{
					Monitor.Pulse(this.m_lockObj);
				}
				else if (this.m_waitCount > 1)
				{
					Monitor.PulseAll(this.m_lockObj);
				}
				if (this.m_waitHandle != null && this.m_currentCount - releaseCount == 0)
				{
					this.m_waitHandle.Set();
				}
				result = this.m_currentCount - releaseCount;
			}
			return result;
		}

		private static int UpdateTimeOut(long startTimeTicks, int originalWaitMillisecondsTimeout)
		{
			long num = (DateTime.UtcNow.Ticks - startTimeTicks) / 10000L;
			if (num > 2147483647L)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}

		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
			}
			return this.Wait((int)timeout.TotalMilliseconds, default(CancellationToken));
		}

		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
			}
			cancellationToken.ThrowIfCancellationRequested();
			long startTimeTicks = 0L;
			if (millisecondsTimeout != -1 && millisecondsTimeout > 0)
			{
				startTimeTicks = DateTime.UtcNow.Ticks;
			}
			bool flag = false;
			CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.Register(SemaphoreSlim.s_cancellationTokenCanceledEventHandler, this);
			try
			{
				SpinWait spinWait = default(SpinWait);
				while (this.m_currentCount == 0)
				{
					if (spinWait.NextSpinWillYield)
					{
						break;
					}
					spinWait.SpinOnce();
				}
				try
				{
				}
				finally
				{
					Monitor2.Enter(this.m_lockObj, ref flag);
					if (flag)
					{
						this.m_waitCount++;
					}
				}
				if (this.m_currentCount == 0)
				{
					if (millisecondsTimeout == 0)
					{
						return false;
					}
					if (!this.WaitUntilCountOrTimeout(millisecondsTimeout, startTimeTicks, cancellationToken))
					{
						return false;
					}
				}
				this.m_currentCount--;
				if (this.m_waitHandle != null && this.m_currentCount == 0)
				{
					this.m_waitHandle.Reset();
				}
			}
			finally
			{
				if (flag)
				{
					this.m_waitCount--;
					Monitor.Exit(this.m_lockObj);
				}
				cancellationTokenRegistration.Dispose();
			}
			return true;
		}

		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
			}
			return this.Wait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		private bool WaitUntilCountOrTimeout(int millisecondsTimeout, long startTimeTicks, CancellationToken cancellationToken)
		{
			int num = -1;
			while (this.m_currentCount == 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (millisecondsTimeout != -1)
				{
					num = SemaphoreSlim.UpdateTimeOut(startTimeTicks, millisecondsTimeout);
					if (num <= 0)
					{
						return false;
					}
				}
				if (!Monitor.Wait(this.m_lockObj, num))
				{
					return false;
				}
			}
			return true;
		}

		public WaitHandle AvailableWaitHandle
		{
			get
			{
				this.CheckDispose();
				if (this.m_waitHandle == null)
				{
					lock (this.m_lockObj)
					{
						if (this.m_waitHandle == null)
						{
							this.m_waitHandle = new ManualResetEvent(this.m_currentCount != 0);
						}
					}
				}
				return this.m_waitHandle;
			}
		}

		public int CurrentCount
		{
			get
			{
				return this.m_currentCount;
			}
		}

		private const int NO_MAXIMUM = 2147483647;

		private volatile int m_currentCount;

		private object m_lockObj;

		private readonly int m_maxCount;

		private volatile int m_waitCount;

		private ManualResetEvent m_waitHandle;

		private static Action<object> s_cancellationTokenCanceledEventHandler = new Action<object>(SemaphoreSlim.CancellationTokenCanceledEventHandler);
	}
}
