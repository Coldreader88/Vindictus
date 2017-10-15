using System;
using System.Runtime.InteropServices;

namespace Devcat.Core.Net.Message
{
	public static class Constants
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct Message
		{
			public const int InstanceOffset = 0;

			public const int CategoryOffset = 8;

			[Obsolete]
			public const int DefaultFlag = -1068630016;

			[Obsolete]
			public const int DefaultFlagMask = -65536;
		}
	}
}
