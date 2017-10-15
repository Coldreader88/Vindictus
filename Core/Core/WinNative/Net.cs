using System;
using System.Runtime.InteropServices;

namespace Devcat.Core.WinNative
{
	public static class Net
	{
		[DllImport("ws2_32", EntryPoint = "WSASend", SetLastError = true)]
		public static extern int WsaSend(IntPtr socket, ref Net.WsaBuffer buffers, int bufferCount, ref int numberOfBytesSent, int flags, ref Net.WsaOverlapped overlapped, Net.WsaOverlappedCompleteRoutine lpCompletionRoutine);

		[DllImport("ws2_32", EntryPoint = "WSARecv", SetLastError = true)]
		public static extern int WsaRecv(IntPtr s, ref Net.WsaBuffer buffers, int bufferCount, ref int numberOfBytesRecvd, ref int flags, ref Net.WsaOverlapped overlapped, Net.WsaOverlappedCompleteRoutine completionRoutine);

		public struct WsaBuffer
		{
			public int Length;

			public IntPtr Buffer;
		}

		public struct WsaOverlapped
		{
			public IntPtr Internal;

			public IntPtr InternalHigh;

			public uint Offset;

			public uint OffsetHigh;

			public IntPtr Event;
		}

		public delegate void WsaOverlappedCompleteRoutine(int error, int transferredCount, ref Net.WsaOverlapped overlapped, int flags);
	}
}
