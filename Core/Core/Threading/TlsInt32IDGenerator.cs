using System;
using System.Threading;

namespace Devcat.Core.Threading
{
	public static class TlsInt32IDGenerator<T>
	{
		public static int GetNextID()
		{
			if ((TlsInt32IDGenerator<T>.id & 65535) == 0)
			{
				TlsInt32IDGenerator<T>.id = Interlocked.Add(ref TlsInt32IDGenerator<T>.bunch, 65536) - 65536;
				if (TlsInt32IDGenerator<T>.id < 0)
				{
					throw new OverflowException("ID pool overflowed.");
				}
			}
			int num = TlsInt32IDGenerator<T>.id;
			TlsInt32IDGenerator<T>.id = num + 1;
			return num;
		}

		private const int bunchSize = 65536;

		private static int bunch;

		[ThreadStatic]
		private static int id;
	}
}
