using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("63623470-8327-455b-A084-A843FA0545CC")]
	[Serializable]
	public sealed class StartProcessMessage
	{
		public string Name { get; private set; }

		public StartProcessMessage(string name)
		{
			this.Name = name;
		}
	}
}
