using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace UnifiedNetwork.ProfileService
{
	public class SystemMonitor
	{
		public SystemMonitor()
		{
			this.memoryCounter = new PerformanceCounter();
			this.cpuCounter = new PerformanceCounter();
			this.InitCounterValue(this.cpuCounter, "Processor", "% Processor Time", "_Total");
			this.diskReadCounter = new PerformanceCounter();
			this.diskWriteCounter = new PerformanceCounter();
			PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
			this.instanceNames = performanceCounterCategory.GetInstanceNames();
			this.netReceivedCounters = new PerformanceCounter[this.instanceNames.Length];
			this.netSentCounters = new PerformanceCounter[this.instanceNames.Length];
			for (int i = 0; i < this.instanceNames.Length; i++)
			{
				this.netReceivedCounters[i] = new PerformanceCounter();
				this.netSentCounters[i] = new PerformanceCounter();
			}
			this.machineStatusCache = new List<LogMachine>();
		}

		public double CpuUse
		{
			get
			{
				return (double)this.cpuCounter.NextValue();
			}
		}

		public double VirtualMemoryUse
		{
			get
			{
				return this.GetCounterValue(this.memoryCounter, "Memory", "Committed Bytes", null);
			}
		}

		public double VirtualMemoryLimit
		{
			get
			{
				return this.GetCounterValue(this.memoryCounter, "Memory", "Commit Limit", null);
			}
		}

		public double AvailablePhysicalMemory
		{
			get
			{
				return this.GetCounterValue(this.memoryCounter, "Memory", "Available Bytes", null);
			}
		}

		public double PhysicalMemoryLimit
		{
			get
			{
				return Convert.ToDouble(this.QueryComputerSystem("totalphysicalmemory"));
			}
		}

		public string GetProcessorUse()
		{
			return ((double)this.cpuCounter.NextValue()).ToString("F") + "%";
		}

		public string GetVirtualMemoryUse()
		{
			string str = this.GetCounterValue(this.memoryCounter, "Memory", "% Committed Bytes In Use", null).ToString("F") + "% (";
			double counterValue = this.GetCounterValue(this.memoryCounter, "Memory", "Committed Bytes", null);
			str = str + this.FormatBytes(counterValue) + " / ";
			counterValue = this.GetCounterValue(this.memoryCounter, "Memory", "Commit Limit", null);
			return str + this.FormatBytes(counterValue) + ") ";
		}

		public string GetPhysicalMemoryUse()
		{
			string text = this.QueryComputerSystem("totalphysicalmemory");
			double num = Convert.ToDouble(text);
			double num2 = this.GetCounterValue(this.memoryCounter, "Memory", "Available Bytes", null);
			num2 = num - num2;
			text = string.Concat(new string[]
			{
				"% (",
				this.FormatBytes(num2),
				" / ",
				this.FormatBytes(num),
				")"
			});
			num2 /= num;
			return (num2 * 100.0).ToString("F") + text;
		}

		public double GetNetworkUse(SystemMonitor.NetworkAccessType accessType)
		{
			if (this.instanceNames.Length == 0)
			{
				return 0.0;
			}
			double result = 0.0;
			for (int i = 0; i < this.instanceNames.Length; i++)
			{
				if (accessType == SystemMonitor.NetworkAccessType.Received)
				{
					result = this.GetCounterValue(this.netReceivedCounters[i], "Network Interface", "Bytes Received/sec", this.instanceNames[i]);
				}
				else if (accessType == SystemMonitor.NetworkAccessType.Sent)
				{
					result = this.GetCounterValue(this.netSentCounters[i], "Network Interface", "Bytes Sent/sec", this.instanceNames[i]);
				}
				else
				{
					if (accessType != SystemMonitor.NetworkAccessType.ReceivedAndSent)
					{
						throw new ArgumentException();
					}
					result = this.GetCounterValue(this.netReceivedCounters[i], "Network Interface", "Bytes Received/sec", this.instanceNames[i]) + this.GetCounterValue(this.netSentCounters[i], "Network Interface", "Bytes Sent/sec", this.instanceNames[i]);
				}
			}
			return result;
		}

		public double GetDiskUse(SystemMonitor.DiskAccessType accessType)
		{
			double result;
			if (accessType == SystemMonitor.DiskAccessType.Read)
			{
				result = this.GetCounterValue(this.diskReadCounter, "PhysicalDisk", "Disk Read Bytes/sec", "_Total");
			}
			else if (accessType == SystemMonitor.DiskAccessType.Write)
			{
				result = this.GetCounterValue(this.diskWriteCounter, "PhysicalDisk", "Disk Write Bytes/sec", "_Total");
			}
			else
			{
				if (accessType != SystemMonitor.DiskAccessType.ReadAndWrite)
				{
					throw new ArgumentException();
				}
				result = this.GetCounterValue(this.diskReadCounter, "PhysicalDisk", "Disk Read Bytes/sec", "_Total") + this.GetCounterValue(this.diskWriteCounter, "PhysicalDisk", "Disk Write Bytes/sec", "_Total");
			}
			return result;
		}

		public string QueryComputerSystem(string type)
		{
			string result = null;
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				result = managementObject[type].ToString();
			}
			return result;
		}

		public string QueryEnvironment(string type)
		{
			return Environment.ExpandEnvironmentVariables(type);
		}

		public string FormatBytes(double bytes)
		{
			int num = 0;
			while (bytes > 1024.0)
			{
				bytes /= 1024.0;
				num++;
			}
			string str = bytes.ToString("F") + " ";
			return str + ((SystemMonitor.Unit)num).ToString();
		}

		private double GetCounterValue(PerformanceCounter perfCounter, string categoryName, string counterName, string instanceName)
		{
			perfCounter.CategoryName = categoryName;
			perfCounter.CounterName = counterName;
			perfCounter.InstanceName = instanceName;
			return (double)perfCounter.NextValue();
		}

		private void InitCounterValue(PerformanceCounter perfCounter, string categoryName, string counterName, string instanceName)
		{
			perfCounter.CategoryName = categoryName;
			perfCounter.CounterName = counterName;
			perfCounter.InstanceName = instanceName;
		}

		public void Update()
		{
			LogMachine item = new LogMachine
			{
				TimeStamp = DateTime.Now,
				CpuUse = this.CpuUse,
				PhysicalMemoryUse = (long)this.AvailablePhysicalMemory,
				PhysicalMemoryLimit = (long)this.PhysicalMemoryLimit,
				VirtualMemoryUse = (long)this.VirtualMemoryUse,
				VirtualMemoryLimit = (long)this.VirtualMemoryLimit,
				NetworkSent = (long)this.GetNetworkUse(SystemMonitor.NetworkAccessType.Sent),
				NetworkReceived = (long)this.GetNetworkUse(SystemMonitor.NetworkAccessType.Received),
				MachineName = Environment.MachineName
			};
			this.machineStatusCache.Add(item);
		}

		public void WriteToDB()
		{
		}

		private PerformanceCounter memoryCounter;

		private PerformanceCounter cpuCounter;

		private PerformanceCounter diskReadCounter;

		private PerformanceCounter diskWriteCounter;

		private string[] instanceNames;

		private PerformanceCounter[] netReceivedCounters;

		private PerformanceCounter[] netSentCounters;

		private List<LogMachine> machineStatusCache;

		public enum NetworkAccessType
		{
			ReceivedAndSent,
			Received,
			Sent
		}

		public enum DiskAccessType
		{
			Read,
			Write,
			ReadAndWrite
		}

		private enum Unit
		{
			B,
			KB,
			MB,
			GB,
			ER
		}
	}
}
