using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("89F2362C-B090-41b7-B48F-C7DFD81A1B98")]
	[Serializable]
	public sealed class WorkGroupChangeMessage
	{
		public string WorkGroup { get; private set; }

		public WorkGroupChangeMessage(string workgroup)
		{
			this.WorkGroup = workgroup;
		}
	}
}
