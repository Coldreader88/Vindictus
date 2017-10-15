using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace Devcat.Core.Threading
{
	[ComVisible(false)]
	[DebuggerDisplay("Participant Count={ParticipantCount},Participants Remaining={ParticipantsRemaining}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Barrier : IDisposable
	{
		public Barrier(int participantCount) : this(participantCount, null)
		{
		}

		public Barrier(int participantCount, Action<Barrier> postPhaseAction)
		{
			if (participantCount < 0 || participantCount > 32767)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, "Barrier_ctor_ArgumentOutOfRange");
			}
			this.m_currentTotalCount = participantCount;
			this.m_postPhaseAction = postPhaseAction;
			this.m_oddEvent = new ManualResetEventSlim(true);
			this.m_evenEvent = new ManualResetEventSlim(false);
			if (postPhaseAction != null && !ExecutionContext.IsFlowSuppressed())
			{
				this.m_ownerThreadContext = ExecutionContext.Capture();
			}
			this.m_actionCallerID = 0;
		}

		public long AddParticipant()
		{
			long result;
			try
			{
				result = this.AddParticipants(1);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new InvalidOperationException("Barrier_AddParticipants_Overflow_ArgumentOutOfRange");
			}
			return result;
		}

        public long AddParticipants(int participantCount)
        {
            this.ThrowIfDisposed();
            if (participantCount < 1)
            {
                throw new ArgumentOutOfRangeException("participantCount", participantCount, "Barrier_AddParticipants_NonPositive_ArgumentOutOfRange");
            }
            if (participantCount > 0x7fff)
            {
                throw new ArgumentOutOfRangeException("participantCount", "Barrier_AddParticipants_Overflow_ArgumentOutOfRange");
            }
            if ((this.m_actionCallerID != 0) && (Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID))
            {
                throw new InvalidOperationException("Barrier_InvalidOperation_CalledFromPHA");
            }
            SpinWait wait = new SpinWait();
            long num = 0L;
            while (true)
            {
                int num3;
                int num4;
                bool flag;
                int currentTotalCount = this.m_currentTotalCount;
                this.GetCurrentTotal(currentTotalCount, out num3, out num4, out flag);
                if ((participantCount + num4) > 0x7fff)
                {
                    throw new ArgumentOutOfRangeException("participantCount", "Barrier_AddParticipants_Overflow_ArgumentOutOfRange");
                }
                if (this.SetCurrentTotal(currentTotalCount, num3, num4 + participantCount, flag))
                {
                    long currentPhase = this.m_currentPhase;
                    num = (flag != ((currentPhase % 2L) == 0L)) ? (currentPhase + 1L) : currentPhase;
                    if (num != currentPhase)
                    {
                        if (flag)
                        {
                            this.m_oddEvent.Wait();
                            return num;
                        }
                        this.m_evenEvent.Wait();
                        return num;
                    }
                    if (flag && this.m_evenEvent.IsSet)
                    {
                        this.m_evenEvent.Reset();
                        return num;
                    }
                    if (!flag && this.m_oddEvent.IsSet)
                    {
                        this.m_oddEvent.Reset();
                    }
                    return num;
                }
                wait.SpinOnce();
            }
        }

        public void Dispose()
		{
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException("Barrier_InvalidOperation_CalledFromPHA");
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					this.m_oddEvent.Dispose();
					this.m_evenEvent.Dispose();
				}
				this.m_disposed = true;
			}
		}

		private void FinishPhase(bool observedSense)
		{
			ContextCallback contextCallback = null;
			if (this.m_postPhaseAction != null)
			{
				try
				{
					try
					{
						this.m_actionCallerID = Thread.CurrentThread.ManagedThreadId;
						if (this.m_ownerThreadContext != null)
						{
							if (contextCallback == null)
							{
								contextCallback = delegate(object i)
								{
									this.m_postPhaseAction(this);
								};
							}
							ExecutionContext.Run(this.m_ownerThreadContext.CreateCopy(), contextCallback, null);
						}
						else
						{
							this.m_postPhaseAction(this);
						}
						this.m_exception = null;
					}
					catch (Exception exception)
					{
						this.m_exception = exception;
					}
					return;
				}
				finally
				{
					this.m_actionCallerID = 0;
					this.SetResetEvents(observedSense);
					if (this.m_exception != null)
					{
						throw new BarrierPostPhaseException(this.m_exception);
					}
				}
			}
			this.SetResetEvents(observedSense);
		}

		private void GetCurrentTotal(int currentTotal, out int current, out int total, out bool sense)
		{
			total = (currentTotal & 32767);
			current = (currentTotal & 2147418112) >> 16;
			sense = ((currentTotal & int.MinValue) == 0);
		}

		public void RemoveParticipant()
		{
			this.RemoveParticipants(1);
		}

        public void RemoveParticipants(int participantCount)
        {
            this.ThrowIfDisposed();
            if (participantCount < 1)
            {
                throw new ArgumentOutOfRangeException("participantCount", participantCount, "Barrier_RemoveParticipants_NonPositive_ArgumentOutOfRange");
            }
            if ((this.m_actionCallerID != 0) && (Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID))
            {
                throw new InvalidOperationException("Barrier_InvalidOperation_CalledFromPHA");
            }
            SpinWait wait = new SpinWait();
            while (true)
            {
                int num2;
                int num3;
                bool flag;
                int currentTotalCount = this.m_currentTotalCount;
                this.GetCurrentTotal(currentTotalCount, out num2, out num3, out flag);
                if (num3 < participantCount)
                {
                    throw new ArgumentOutOfRangeException("participantCount", "Barrier_RemoveParticipants_ArgumentOutOfRange");
                }
                if ((num3 - participantCount) < num2)
                {
                    throw new InvalidOperationException("Barrier_RemoveParticipants_InvalidOperation");
                }
                int num4 = num3 - participantCount;
                if ((num4 > 0) && (num2 == num4))
                {
                    if (this.SetCurrentTotal(currentTotalCount, 0, num3 - participantCount, !flag))
                    {
                        this.FinishPhase(flag);
                        return;
                    }
                }
                else if (this.SetCurrentTotal(currentTotalCount, num2, num3 - participantCount, flag))
                {
                    return;
                }
                wait.SpinOnce();
            }
        }

        private bool SetCurrentTotal(int currentTotal, int current, int total, bool sense)
		{
			int num = current << 16 | total;
			if (!sense)
			{
				num |= int.MinValue;
			}
			return Interlocked.CompareExchange(ref this.m_currentTotalCount, num, currentTotal) == currentTotal;
		}

		private void SetResetEvents(bool observedSense)
		{
			this.m_currentPhase += 1L;
			if (observedSense)
			{
				this.m_oddEvent.Reset();
				this.m_evenEvent.Set();
				return;
			}
			this.m_evenEvent.Reset();
			this.m_oddEvent.Set();
		}

		public void SignalAndWait()
		{
			this.SignalAndWait(default(System.Threading.CancellationToken));
		}

		public bool SignalAndWait(int millisecondsTimeout)
		{
			return this.SignalAndWait(millisecondsTimeout, default(System.Threading.CancellationToken));
		}

		public void SignalAndWait(System.Threading.CancellationToken cancellationToken)
		{
			this.SignalAndWait(-1, cancellationToken);
		}

		public bool SignalAndWait(TimeSpan timeout)
		{
			return this.SignalAndWait(timeout, default(System.Threading.CancellationToken));
		}

        public bool SignalAndWait(int millisecondsTimeout, System.Threading.CancellationToken cancellationToken)
        {
            int currentTotalCount;
            int num2;
            int num3;
            bool flag;
            this.ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();
            if (millisecondsTimeout < -1)
            {
                throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, "Barrier_SignalAndWait_ArgumentOutOfRange");
            }
            if ((this.m_actionCallerID != 0) && (Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID))
            {
                throw new InvalidOperationException("Barrier_InvalidOperation_CalledFromPHA");
            }
            SpinWait wait = new SpinWait();
            while (true)
            {
                currentTotalCount = this.m_currentTotalCount;
                this.GetCurrentTotal(currentTotalCount, out num2, out num3, out flag);
                if (num3 == 0)
                {
                    throw new InvalidOperationException("Barrier_SignalAndWait_InvalidOperation_ZeroTotal");
                }
                if ((num2 == 0) && (flag != ((this.m_currentPhase % 2L) == 0L)))
                {
                    throw new InvalidOperationException("Barrier_SignalAndWait_InvalidOperation_ThreadsExceeded");
                }
                if ((num2 + 1) == num3)
                {
                    if (this.SetCurrentTotal(currentTotalCount, 0, num3, !flag))
                    {
                        this.FinishPhase(flag);
                        return true;
                    }
                }
                else if (this.SetCurrentTotal(currentTotalCount, num2 + 1, num3, flag))
                {
                    break;
                }
                wait.SpinOnce();
            }
            long currentPhase = this.m_currentPhase;
            ManualResetEventSlim slim = flag ? this.m_evenEvent : this.m_oddEvent;
            bool flag2 = false;
            bool flag3 = false;
            try
            {
                flag3 = slim.Wait(millisecondsTimeout, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                flag2 = true;
            }
            if (!flag3)
            {
                wait.Reset();
                while (true)
                {
                    bool flag4;
                    currentTotalCount = this.m_currentTotalCount;
                    this.GetCurrentTotal(currentTotalCount, out num2, out num3, out flag4);
                    if ((currentPhase != this.m_currentPhase) || (flag != flag4))
                    {
                        slim.Wait();
                        break;
                    }
                    if (this.SetCurrentTotal(currentTotalCount, num2 - 1, num3, flag))
                    {
                        if (flag2)
                        {
                            throw new OperationCanceledException2("Common_OperationCanceled", cancellationToken);
                        }
                        return false;
                    }
                    wait.SpinOnce();
                }
            }
            if (this.m_exception != null)
            {
                throw new Devcat.Core.Threading.BarrierPostPhaseException(this.m_exception);
            }
            return true;
        }

        public bool SignalAndWait(TimeSpan timeout, System.Threading.CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, "Barrier_SignalAndWait_ArgumentOutOfRange");
			}
			return this.SignalAndWait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		private void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Barrier", "Barrier_Dispose");
			}
		}

		public long CurrentPhaseNumber
		{
			get
			{
				return this.m_currentPhase;
			}
		}

		public int ParticipantCount
		{
			get
			{
				return this.m_currentTotalCount & 32767;
			}
		}

		public int ParticipantsRemaining
		{
			get
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num = currentTotalCount & 32767;
				int num2 = (currentTotalCount & 2147418112) >> 16;
				return num - num2;
			}
		}

		private const int CURRENT_MASK = 2147418112;

		private const int MAX_PARTICIPANTS = 32767;

		private const int SENSE_MASK = -2147483648;

		private const int TOTAL_MASK = 32767;

		private int m_actionCallerID;

		private long m_currentPhase;

		private volatile int m_currentTotalCount;

		private bool m_disposed;

		private ManualResetEventSlim m_evenEvent;

		private Exception m_exception;

		private ManualResetEventSlim m_oddEvent;

		private ExecutionContext m_ownerThreadContext;

		private Action<Barrier> m_postPhaseAction;
	}
}
