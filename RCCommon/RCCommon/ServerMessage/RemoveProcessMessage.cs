using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[MutexCommand]
	[Guid("FA0C012E-9520-405a-AA40-9B08B73CD9EA")]
	[Serializable]
	public sealed class RemoveProcessMessage
	{
		public string Name { get; private set; }

		public RemoveProcessMessage(string name)
		{
			this.Name = name;
		}
	}
}
