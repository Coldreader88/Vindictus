using System;
using System.Runtime.InteropServices;

namespace Devcat.Core.WinNative
{
	public class Thread
	{
		[DllImport("kernel32", ExactSpelling = true)]
		public static extern void SwitchToThread();

		[DllImport("kernel32", ExactSpelling = true)]
		public static extern void SetThreadIdealProcessor(IntPtr threadHandle, uint idealProcessor);
	}
}
