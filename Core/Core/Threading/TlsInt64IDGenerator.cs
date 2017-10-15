using System;
using System.Threading;

namespace Devcat.Core.Threading
{
	public static class TlsInt64IDGenerator<T>
	{
		public static long GetNextID()
		{
			long num = TlsInt64IDGenerator<T>.id;
			if (0L == 0L)
			{
				TlsInt64IDGenerator<T>.id = Interlocked.Add(ref TlsInt64IDGenerator<T>.bunch, 1L) - 1L;
				if (TlsInt64IDGenerator<T>.id < 0L)
				{
					throw new OverflowException("ID pool overflowed.");
				}
			}
			long num2 = TlsInt64IDGenerator<T>.id;
			TlsInt64IDGenerator<T>.id = num2 + 1L;
			return num2;
		}

		private const long bunchSize = 1L;

		private static long bunch;

		[ThreadStatic]
		private static long id;
	}
}
