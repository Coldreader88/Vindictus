using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[MutexCommand]
	[Guid("A3A79991-CF86-428a-9052-4EE0FD1D820C")]
	[Serializable]
	public sealed class AddProcessMessage
	{
		public RCProcess Process { get; private set; }

		public AddProcessMessage(RCProcess process)
		{
			this.Process = process;
		}
	}
}
