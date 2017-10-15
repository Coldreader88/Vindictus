using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("F4557B43-C2C2-45bd-9FF0-8C31C15858F3")]
	[Serializable]
	public sealed class KillUpdateProcessMessage
	{
		public string Name { get; private set; }

		public KillUpdateProcessMessage(string name)
		{
			this.Name = name;
		}
	}
}
