using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[MutexCommand]
	[Guid("1EF6DCE2-AD72-4275-B630-623655C47CD6")]
	[Serializable]
	public sealed class ModifyProcessMessage
	{
		public RCProcess Process { get; private set; }

		public ModifyProcessMessage(RCProcess process)
		{
			this.Process = process;
		}
	}
}
