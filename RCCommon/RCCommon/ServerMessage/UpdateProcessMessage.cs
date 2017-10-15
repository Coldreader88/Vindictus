using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("96F2A65E-EF6C-4078-A6A8-84A022B94F5B")]
	[Serializable]
	public sealed class UpdateProcessMessage
	{
		public string Name { get; private set; }

		public UpdateProcessMessage(string name)
		{
			this.Name = name;
		}
	}
}
