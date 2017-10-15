using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("88C6763C-A115-4ff1-8B72-6A32DE411FFD")]
	[Serializable]
	public sealed class ControlRequestMessage
	{
		public ArraySegment<byte> Packet { get; private set; }

		public IEnumerable<int> IDs
		{
			get
			{
				return this.idList;
			}
		}

		public ControlRequestMessage(ArraySegment<byte> packet)
		{
			this.Packet = packet;
			this.idList = new List<int>();
		}

		public void AddClientID(int id)
		{
			this.idList.Add(id);
		}

		private List<int> idList;
	}
}
