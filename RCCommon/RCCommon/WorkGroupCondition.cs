using System;

namespace RemoteControlSystem
{
	public class WorkGroupCondition : IWorkGroupCondition, IComparable
	{
		public string ClientName { get; private set; }

		public string ProcessName { get; private set; }

		public WorkGroupCondition()
		{
		}

		public WorkGroupCondition(string clientName, string processName)
		{
			if (clientName == null)
			{
				throw new ArgumentNullException("ClientName", "ClientName cannot be null!");
			}
			if (processName == null)
			{
				throw new ArgumentNullException("ProcessName", "ProcessName cannot be null!");
			}
			this.ClientName = clientName;
			this.ProcessName = processName;
		}

		public override string ToString()
		{
			return this.ToString(this.ClientName, this.ProcessName);
		}

		public string ToString(string clientName, string processName)
		{
			return string.Format("{0} - {1}", clientName, processName);
		}

		public int CompareTo(object obj)
		{
			int num = this.ClientName.CompareTo(((WorkGroupCondition)obj).ClientName);
			if (num == 0)
			{
				return this.ProcessName.CompareTo(((WorkGroupCondition)obj).ProcessName);
			}
			return num;
		}
	}
}
