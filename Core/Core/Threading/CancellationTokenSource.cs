using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace Devcat.Core.Threading
{
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class CancellationTokenSource : IDisposable
	{
		public CancellationTokenSource()
		{
			this.m_threadIDExecutingCallbacks = -1;
			this.m_state = 1;
		}

		private CancellationTokenSource(bool set)
		{
			this.m_threadIDExecutingCallbacks = -1;
			this.m_state = (set ? 3 : 0);
		}

		public void Cancel()
		{
			this.Cancel(false);
		}

		public void Cancel(bool throwOnFirstException)
		{
			this.ThrowIfDisposed();
			this.NotifyCancellation(throwOnFirstException);
		}

		private void CancellationCallbackCoreWork_OnSyncContext(object obj)
		{
			CancellationCallbackCoreWorkArguments cancellationCallbackCoreWorkArguments = (CancellationCallbackCoreWorkArguments)obj;
			CancellationCallbackInfo cancellationCallbackInfo = cancellationCallbackCoreWorkArguments.m_currArrayFragment.SafeAtomicRemove(cancellationCallbackCoreWorkArguments.m_currArrayIndex, this.m_executingCallback);
			if (cancellationCallbackInfo == this.m_executingCallback)
			{
				if (cancellationCallbackInfo.TargetExecutionContext != null)
				{
					cancellationCallbackInfo.CancellationTokenSource.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
				}
				cancellationCallbackInfo.ExecuteCallback();
			}
		}

		public static CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] tokens)
		{
			if (tokens == null)
			{
				throw new ArgumentNullException("tokens");
			}
			if (tokens.Length == 0)
			{
				throw new ArgumentException(Environment2.GetResourceString("CancellationToken_CreateLinkedToken_TokensIsEmpty"));
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource
			{
				m_linkingRegistrations = new List<CancellationTokenRegistration>()
			};
			for (int i = 0; i < tokens.Length; i++)
			{
				if (tokens[i].CanBeCanceled)
				{
					cancellationTokenSource.m_linkingRegistrations.Add(tokens[i].InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource));
				}
			}
			return cancellationTokenSource;
		}

		public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token1, CancellationToken token2)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			if (token1.CanBeCanceled)
			{
				cancellationTokenSource.m_linkingRegistrations = new List<CancellationTokenRegistration>();
				cancellationTokenSource.m_linkingRegistrations.Add(token1.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource));
			}
			if (token2.CanBeCanceled)
			{
				if (cancellationTokenSource.m_linkingRegistrations == null)
				{
					cancellationTokenSource.m_linkingRegistrations = new List<CancellationTokenRegistration>();
				}
				cancellationTokenSource.m_linkingRegistrations.Add(token2.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource));
			}
			return cancellationTokenSource;
		}

		public void Dispose()
		{
			if (!this.m_disposed)
			{
				if (this.m_linkingRegistrations != null)
				{
					foreach (CancellationTokenRegistration cancellationTokenRegistration in this.m_linkingRegistrations)
					{
						cancellationTokenRegistration.Dispose();
					}
					this.m_linkingRegistrations = null;
				}
				this.m_registeredCallbacksLists = null;
				if (this.m_kernelEvent != null)
				{
					this.m_kernelEvent.Close();
					this.m_kernelEvent = null;
				}
				this.m_disposed = true;
			}
		}

		private void ExecuteCallbackHandlers(bool throwOnFirstException)
		{
			List<Exception> list = null;
			SparselyPopulatedArray<CancellationCallbackInfo>[] registeredCallbacksLists = this.m_registeredCallbacksLists;
			if (registeredCallbacksLists == null)
			{
				Interlocked.Exchange(ref this.m_state, 3);
				return;
			}
			try
			{
				foreach (SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray in registeredCallbacksLists)
				{
					if (sparselyPopulatedArray != null)
					{
						for (SparselyPopulatedArrayFragment<CancellationCallbackInfo> sparselyPopulatedArrayFragment = sparselyPopulatedArray.Tail; sparselyPopulatedArrayFragment != null; sparselyPopulatedArrayFragment = sparselyPopulatedArrayFragment.Prev)
						{
							for (int j = sparselyPopulatedArrayFragment.Length - 1; j >= 0; j--)
							{
								this.m_executingCallback = sparselyPopulatedArrayFragment[j];
								if (this.m_executingCallback != null)
								{
									CancellationCallbackCoreWorkArguments cancellationCallbackCoreWorkArguments = new CancellationCallbackCoreWorkArguments(sparselyPopulatedArrayFragment, j);
									try
									{
										if (this.m_executingCallback.TargetSyncContext != null)
										{
											this.m_executingCallback.TargetSyncContext.Send(new SendOrPostCallback(this.CancellationCallbackCoreWork_OnSyncContext), cancellationCallbackCoreWorkArguments);
											this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
										}
										else
										{
											this.CancellationCallbackCoreWork_OnSyncContext(cancellationCallbackCoreWorkArguments);
										}
									}
									catch (Exception item)
									{
										if (throwOnFirstException)
										{
											throw;
										}
										if (list == null)
										{
											list = new List<Exception>();
										}
										list.Add(item);
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this.m_state = 3;
				this.m_executingCallback = null;
				Thread.MemoryBarrier();
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		internal static CancellationTokenSource InternalGetStaticSource(bool set)
		{
			if (!set)
			{
				return CancellationTokenSource._staticSource_NotCancelable;
			}
			return CancellationTokenSource._staticSource_Set;
		}

		internal CancellationTokenRegistration InternalRegister(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext executionContext)
		{
			this.ThrowIfDisposed();
			if (!this.IsCancellationRequested)
			{
				int num = Thread.CurrentThread.ManagedThreadId % CancellationTokenSource.s_nLists;
				CancellationCallbackInfo cancellationCallbackInfo = new CancellationCallbackInfo(callback, stateForCallback, targetSyncContext, executionContext, this);
				if (this.m_registeredCallbacksLists == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo>[] value = new SparselyPopulatedArray<CancellationCallbackInfo>[CancellationTokenSource.s_nLists];
					Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>[]>(ref this.m_registeredCallbacksLists, value, null);
				}
				if (this.m_registeredCallbacksLists[num] == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> value2 = new SparselyPopulatedArray<CancellationCallbackInfo>(4);
					Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>>(ref this.m_registeredCallbacksLists[num], value2, null);
				}
				SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo = this.m_registeredCallbacksLists[num].Add(cancellationCallbackInfo);
				CancellationTokenRegistration result = new CancellationTokenRegistration(this, cancellationCallbackInfo, registrationInfo);
				if (!this.IsCancellationRequested)
				{
					return result;
				}
				if (!result.TryDeregister())
				{
					this.WaitForCallbackToComplete(cancellationCallbackInfo);
					return default(CancellationTokenRegistration);
				}
			}
			callback(stateForCallback);
			return default(CancellationTokenRegistration);
		}

		private static void LinkedTokenCancelDelegate(object source)
		{
			(source as CancellationTokenSource).Cancel();
		}

		private void NotifyCancellation(bool throwOnFirstException)
		{
			if (!this.IsCancellationRequested && Interlocked.CompareExchange(ref this.m_state, 2, 1) == 1)
			{
				this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
				if (this.m_kernelEvent != null)
				{
					this.m_kernelEvent.Set();
				}
				this.ExecuteCallbackHandlers(throwOnFirstException);
			}
		}

		internal void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(null, Environment2.GetResourceString("CancellationTokenSource_Disposed"));
			}
		}

		internal void WaitForCallbackToComplete(CancellationCallbackInfo callbackInfo)
		{
			SpinWait spinWait = default(SpinWait);
			while (this.ExecutingCallback == callbackInfo)
			{
				spinWait.SpinOnce();
			}
		}

		internal bool CanBeCanceled
		{
			get
			{
				return this.m_state != 0;
			}
		}

		internal CancellationCallbackInfo ExecutingCallback
		{
			get
			{
				return this.m_executingCallback;
			}
		}

		internal bool IsCancellationCompleted
		{
			get
			{
				return this.m_state == 3;
			}
		}

		public bool IsCancellationRequested
		{
			get
			{
				return this.m_state >= 2;
			}
		}

		internal bool IsDisposed
		{
			get
			{
				return this.m_disposed;
			}
		}

		internal int ThreadIDExecutingCallbacks
		{
			get
			{
				return this.m_threadIDExecutingCallbacks;
			}
			set
			{
				this.m_threadIDExecutingCallbacks = value;
			}
		}

		public CancellationToken Token
		{
			get
			{
				this.ThrowIfDisposed();
				return new CancellationToken(this);
			}
		}

		internal WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.m_kernelEvent == null)
				{
					ManualResetEvent manualResetEvent = new ManualResetEvent(false);
					if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_kernelEvent, manualResetEvent, null) != null)
					{
						((IDisposable)manualResetEvent).Dispose();
					}
					if (this.IsCancellationRequested)
					{
						this.m_kernelEvent.Set();
					}
				}
				return this.m_kernelEvent;
			}
		}

		private const int CANNOT_BE_CANCELED = 0;

		private const int NOT_CANCELED = 1;

		private const int NOTIFYING = 2;

		private const int NOTIFYINGCOMPLETE = 3;

		private static readonly CancellationTokenSource _staticSource_NotCancelable = new CancellationTokenSource(false);

		private static readonly CancellationTokenSource _staticSource_Set = new CancellationTokenSource(true);

		private bool m_disposed;

		private volatile CancellationCallbackInfo m_executingCallback;

		private volatile ManualResetEvent m_kernelEvent;

		private List<CancellationTokenRegistration> m_linkingRegistrations;

		private volatile SparselyPopulatedArray<CancellationCallbackInfo>[] m_registeredCallbacksLists;

		private volatile int m_state;

		private volatile int m_threadIDExecutingCallbacks;

		private static readonly Action<object> s_LinkedTokenCancelDelegate = new Action<object>(CancellationTokenSource.LinkedTokenCancelDelegate);

		private static readonly int s_nLists = (PlatformHelper.ProcessorCount > 24) ? 24 : PlatformHelper.ProcessorCount;
	}
}
