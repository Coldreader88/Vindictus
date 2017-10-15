using System;
using System.Runtime.InteropServices;

namespace Devcat.Core.Threading
{
	internal static class Platform
	{
		[DllImport("kernel32.dll")]
		private static extern int SwitchToThread();

		internal static void Yield()
		{
			Platform.SwitchToThread();
		}

		internal static bool IsSingleProcessor
		{
			get
			{
				return Platform.ProcessorCount == 1;
			}
		}

		internal static int ProcessorCount
		{
			get
			{
				return Environment.ProcessorCount;
			}
		}
	}
}
