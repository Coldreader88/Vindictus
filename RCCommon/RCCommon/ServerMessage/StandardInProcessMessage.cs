using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("DC132D3F-6FFC-46ef-8F26-A480EF0A677F")]
	[Serializable]
	public sealed class StandardInProcessMessage
	{
		public string Name { get; private set; }

		public string Message { get; private set; }

		public StandardInProcessMessage(string name, string message)
		{
			this.Name = name;
			this.Message = message;
		}
	}
}
