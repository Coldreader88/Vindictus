using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("96C1B84D-CC4C-41a5-85F9-B8F314599DF8")]
	[Serializable]
	public sealed class LogProcessMessage
	{
		public string Name { get; private set; }

		public string Message { get; private set; }

		public LogProcessMessage(string name, string message)
		{
			this.Name = name;
			this.Message = message;
		}
	}
}
