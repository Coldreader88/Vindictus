using System;
using System.Runtime.InteropServices;

namespace Devcat.Core.Threading
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct StackCrawlMark2
	{
		internal static StackCrawlMark2 LookForMyCaller
		{
			get
			{
				return default(StackCrawlMark2);
			}
		}
	}
}
