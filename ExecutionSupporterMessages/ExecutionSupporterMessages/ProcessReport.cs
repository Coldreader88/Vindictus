using System;

namespace ExecutionSupporterMessages
{
	[Serializable]
	public sealed class ProcessReport
	{
		public string ProcessName { get; set; }

		public float Memory { get; set; }

		public float CPU { get; set; }

		public bool IsResponding { get; set; }

		public ProcessReport(string name, float memory, float cpu, bool isResponding)
		{
			this.ProcessName = name;
			this.Memory = memory;
			this.CPU = cpu;
			this.IsResponding = isResponding;
		}
	}
}
