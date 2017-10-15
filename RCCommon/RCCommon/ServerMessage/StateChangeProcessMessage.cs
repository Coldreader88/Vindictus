using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("F360A29B-98D2-44dc-A1AF-0674AD3E6456")]
	[Serializable]
	public sealed class StateChangeProcessMessage
	{
		public string Name { get; private set; }

		public RCProcess.ProcessState State { get; private set; }

		public DateTime ChangedTime { get; private set; }

		public StateChangeProcessMessage(string name, RCProcess.ProcessState state, DateTime changedTime)
		{
			this.Name = name;
			this.State = state;
			this.ChangedTime = changedTime;
		}
	}
}
