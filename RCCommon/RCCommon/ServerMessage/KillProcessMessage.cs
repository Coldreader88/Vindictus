using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("1B589328-ABFB-445e-B999-F74D75DB175E")]
	[Serializable]
	public sealed class KillProcessMessage
	{
		public string Name { get; private set; }

		public KillProcessMessage(string name)
		{
			this.Name = name;
		}
	}
}
