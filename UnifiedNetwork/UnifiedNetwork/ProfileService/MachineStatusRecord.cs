using System;

namespace UnifiedNetwork.ProfileService
{
	public class MachineStatusRecord
	{
		public DateTime TimeStamp { get; set; }

		public int CpuUse { get; set; }

		public int VirtualMemoryUse { get; set; }

		public int PhysicalMemoryUse { get; set; }

		public int NetReceived { get; set; }

		public int NetSent { get; set; }
	}
}
