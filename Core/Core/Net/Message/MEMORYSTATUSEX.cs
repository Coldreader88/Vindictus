using System;
using System.Runtime.InteropServices;

namespace Devcat.Core.Net.Message
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal class MEMORYSTATUSEX
	{
		public MEMORYSTATUSEX()
		{
			this.dwLength = (uint)Marshal.SizeOf(this);
		}

		public uint dwLength;

		public uint dwMemoryLoad;

		public ulong ullTotalPhys;

		public ulong ullAvailPhys;

		public ulong ullTotalPageFile;

		public ulong ullAvailPageFile;

		public ulong ullTotalVirtual;

		public ulong ullAvailVirtual;

		public ulong ullAvailExtendedVirtual;
	}
}
