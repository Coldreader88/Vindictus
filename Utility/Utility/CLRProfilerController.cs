using System;
using System.Runtime.InteropServices;

namespace Utility
{
	public class CLRProfilerController
	{
		[DllImport("ProfilerOBJ.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		private static extern bool SetEnableLogging(bool enableLog);

		public static bool TrySetEnableLogging(bool enableLog)
		{
			try
			{
				return CLRProfilerController.SetEnableLogging(enableLog);
			}
			catch (Exception ex)
			{
				Log<CLRProfilerController>.Logger.Error("CLRProfilerController.TrySetEnableLogging is failed.", ex);
			}
			return false;
		}
	}
}
