using System;
using System.Collections.Generic;
using RemoteControlSystem.ControlMessage;

namespace RemoteControlSystem.Server
{
	public class ChildProcessLogEventArgs : EventArgs
	{
		public IEnumerable<int> Clients
		{
			get
			{
				return this.clients;
			}
		}

		public ChildProcessLogEventArgs(IEnumerable<int> _clients, ChildProcessLogMessage _message)
		{
			this.clients = _clients;
			this.Message = _message;
		}

		private IEnumerable<int> clients;

		public ChildProcessLogMessage Message;
	}
}
