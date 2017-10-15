using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("FAAEAE90-7BF0-4985-B9B4-6C32D1D30BF4")]
	[Serializable]
	public class AdminReportNotifyMessage
	{
		public NotifyCode Code { get; set; }

		public string Message { get; set; }

		public AdminReportNotifyMessage(NotifyCode code, string msg)
		{
			this.Code = code;
			this.Message = msg;
		}

		public override string ToString()
		{
			return string.Format("AdminReportNotifyMessage [{0}/{1}]", this.Code, this.Message);
		}
	}
}
