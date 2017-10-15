using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("E0B2D578-50C0-486d-8CE3-C73692908935")]
	[Serializable]
	public class AdminRequestEmergencyStopMessage
	{
		public string TargetService
		{
			get
			{
				return this.targetservice;
			}
			set
			{
				this.targetservice = value;
			}
		}

		public bool TargetState
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		public AdminRequestEmergencyStopMessage()
		{
		}

		public AdminRequestEmergencyStopMessage(string target, bool state)
		{
			this.targetservice = target;
			this.state = state;
		}

		public override string ToString()
		{
			return string.Format("AdminRequestEmergencyStopMessage [{0}]", this.targetservice);
		}

		private string targetservice;

		private bool state;
	}
}
