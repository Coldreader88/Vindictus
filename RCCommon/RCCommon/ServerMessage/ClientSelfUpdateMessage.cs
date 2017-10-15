using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[MutexCommand]
	[Guid("AC9A2FA2-F54E-4fd8-8F85-753BE78F4537")]
	[Serializable]
	public sealed class ClientSelfUpdateMessage
	{
		public string Argument { get; private set; }

		public ClientSelfUpdateMessage(string argument)
		{
			this.Argument = argument;
		}
	}
}
