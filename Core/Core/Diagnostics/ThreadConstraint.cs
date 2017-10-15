using System;
using System.Diagnostics;
using System.Threading;

namespace Devcat.Core.Diagnostics
{
	public static class ThreadConstraint
	{
		[Conditional("DEBUG")]
		public static void EnterCriticalSection(object syncObj)
		{
			if (!Monitor.TryEnter(syncObj))
			{
				ThreadConstraint.syncError = true;
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
				throw new InvalidOperationException("This context is not thread safe.");
			}
		}

		[Conditional("DEBUG")]
		public static void LeaveCriticalSection(object syncObj)
		{
			Monitor.Exit(syncObj);
			if (ThreadConstraint.syncError)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
				throw new InvalidOperationException("This context is not thread safe.");
			}
		}

		private static volatile bool syncError;
	}
}
