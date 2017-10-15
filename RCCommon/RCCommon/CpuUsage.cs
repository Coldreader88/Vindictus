using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace RemoteControlSystem
{
	public class CpuUsage
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetSystemTimes(out System.Runtime.InteropServices.ComTypes.FILETIME lpIdleTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpKernelTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpUserTime);

		public float CalcUsage(Process process, out float kernelUsage, out float userUsage)
		{
			float result = 0f;
			kernelUsage = 0f;
			userUsage = 0f;
			TimeSpan totalProcessorTime = process.TotalProcessorTime;
			TimeSpan privilegedProcessorTime = process.PrivilegedProcessorTime;
			TimeSpan userProcessorTime = process.UserProcessorTime;
			System.Runtime.InteropServices.ComTypes.FILETIME filetime;
			System.Runtime.InteropServices.ComTypes.FILETIME filetime2;
			System.Runtime.InteropServices.ComTypes.FILETIME filetime3;
			if (!CpuUsage.GetSystemTimes(out filetime, out filetime2, out filetime3))
			{
				return 0f;
			}
			if (this.PrevProcTotal != TimeSpan.MinValue)
			{
				ulong num = this.SubtractTimes(filetime2, this.PrevSysKernel);
				ulong num2 = this.SubtractTimes(filetime3, this.PrevSysUser);
				ulong num3 = this.SubtractTimes(filetime, this.PrevSysIdle);
				ulong num4 = num + num2;
				long num5 = (long)(num - num3);
				if (num5 < 0L)
				{
				}
				long num6 = totalProcessorTime.Ticks - this.PrevProcTotal.Ticks;
				long num7 = privilegedProcessorTime.Ticks - this.PrevProcKernel.Ticks;
				long num8 = userProcessorTime.Ticks - this.PrevProcUser.Ticks;
				if (num4 > 0UL)
				{
					result = (float)((short)(100.0 * (double)num6 / num4));
				}
				if (num6 > 0L)
				{
					kernelUsage = (float)(100.0 * (double)num7 / (double)num6);
					userUsage = (float)(100.0 * (double)num8 / (double)num6);
				}
			}
			this.PrevProcTotal = totalProcessorTime;
			this.PrevProcKernel = privilegedProcessorTime;
			this.PrevProcUser = userProcessorTime;
			this.PrevSysKernel = filetime2;
			this.PrevSysUser = filetime3;
			this.PrevSysIdle = filetime;
			return result;
		}

		private ulong SubtractTimes(System.Runtime.InteropServices.ComTypes.FILETIME a, System.Runtime.InteropServices.ComTypes.FILETIME b)
		{
            return ((ulong)a.dwHighDateTime << 32 | (ulong)(uint)a.dwLowDateTime) - ((ulong)b.dwHighDateTime << 32 | (ulong)(uint)b.dwLowDateTime);
        }

		private TimeSpan PrevProcTotal = TimeSpan.MinValue;

		private TimeSpan PrevProcKernel = TimeSpan.MinValue;

		private TimeSpan PrevProcUser = TimeSpan.MinValue;

		private System.Runtime.InteropServices.ComTypes.FILETIME PrevSysKernel;

		private System.Runtime.InteropServices.ComTypes.FILETIME PrevSysUser;

		private System.Runtime.InteropServices.ComTypes.FILETIME PrevSysIdle;
	}
}
