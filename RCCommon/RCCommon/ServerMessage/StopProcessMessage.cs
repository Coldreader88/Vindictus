using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("B7A5B0B7-48BF-4d1a-95E8-FC6AC8271DFB")]
	[Serializable]
	public sealed class StopProcessMessage
	{
		public string Name { get; private set; }

		public StopProcessMessage(string name)
		{
			this.Name = name;
		}
	}
}
