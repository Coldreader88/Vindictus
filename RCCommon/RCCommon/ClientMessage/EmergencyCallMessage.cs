using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("586802E1-2243-4cdb-943E-7A622262AE46")]
	[Serializable]
	public sealed class EmergencyCallMessage
	{
		public IEnumerable<string> Emergencies
		{
			get
			{
				return this.emergencies;
			}
		}

		public EmergencyCallMessage()
		{
			this.emergencies = new List<string>();
		}

		public void AddEmergencyCallInfo(string department, string id, string name, string phoneNumber, string mail, string rank)
		{
			this.emergencies.Add(department);
			this.emergencies.Add(id);
			this.emergencies.Add(name);
			this.emergencies.Add(phoneNumber);
			this.emergencies.Add(mail);
			this.emergencies.Add(rank);
		}

		private List<string> emergencies;
	}
}
