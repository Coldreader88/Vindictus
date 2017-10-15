using System;
using System.Threading;

namespace Devcat.Core.Threading
{
	internal class CancellationCallbackInfo
	{
		internal CancellationCallbackInfo(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource)
		{
			this.Callback = callback;
			this.StateForCallback = stateForCallback;
			this.TargetSyncContext = targetSyncContext;
			this.TargetExecutionContext = targetExecutionContext;
			this.CancellationTokenSource = cancellationTokenSource;
		}

		[SecuritySafeCritical]
		internal void ExecuteCallback()
		{
			if (this.TargetExecutionContext != null)
			{
				ExecutionContext.Run(this.TargetExecutionContext, new ContextCallback(CancellationCallbackInfo.ExecutionContextCallback), this);
				return;
			}
			CancellationCallbackInfo.ExecutionContextCallback(this);
		}

		private static void ExecutionContextCallback(object obj)
		{
			CancellationCallbackInfo cancellationCallbackInfo = obj as CancellationCallbackInfo;
			cancellationCallbackInfo.Callback(cancellationCallbackInfo.StateForCallback);
		}

		internal readonly Action<object> Callback;

		internal readonly CancellationTokenSource CancellationTokenSource;

		internal readonly object StateForCallback;

		internal readonly ExecutionContext TargetExecutionContext;

		internal readonly SynchronizationContext TargetSyncContext;
	}
}
