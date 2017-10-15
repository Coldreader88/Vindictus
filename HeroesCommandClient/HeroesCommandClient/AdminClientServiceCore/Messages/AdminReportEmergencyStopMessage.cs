using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("E5864465-7083-4d8f-8EE4-E84FBBAEF477")]
	[Serializable]
	public class AdminReportEmergencyStopMessage
	{
		public List<string> ServiceList
		{
			get
			{
				return this.servicelist;
			}
			set
			{
				this.servicelist = value;
			}
		}

		public AdminReportEmergencyStopMessage()
		{
			this.servicelist = new List<string>();
		}

		public override string ToString()
		{
			return string.Format("AdminReportEmergencyStopMessage", new object[0]);
		}

		private List<string> servicelist;
	}
}
