using System;
using System.Runtime.InteropServices;

namespace Devcat.Core.WinNative
{
	public static class IO
	{
		[DllImport("kernel32", EntryPoint = "CreateIoCompletionPort")]
		public static extern IntPtr CreateIOCompletionPort(IntPtr fileHandle, IntPtr existingCompletionPort, IntPtr completionKey, int numberOfConcurrentThreads);

		[DllImport("kernel32")]
		public static extern bool PostQueuedCompletionStatus(IntPtr completionPort, uint numberOfBytesTransferred, IntPtr completionKey, ref IO.Overlapped overlapped);

		[DllImport("kernel32")]
		public static extern bool GetQueuedCompletionStatus(IntPtr completionPort, out int numberOfBytes, out IntPtr completionKey, out IO.Overlapped overlapped, int milliseconds);

		public struct Overlapped
		{
			public IntPtr Internal;

			public IntPtr InternalHigh;

			public uint Offset;

			public uint OffsetHigh;

			public IntPtr hEvent;
		}
	}
}
