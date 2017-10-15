using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("C3623470-E327-A55b-A084-A843FA0545CF")]
	[Serializable]
	public sealed class CheckPatchProcessMessage
	{
		public List<string> Name { get; private set; }

		public CheckPatchProcessMessage(List<string> name)
		{
			this.Name = name;
		}
	}
}
