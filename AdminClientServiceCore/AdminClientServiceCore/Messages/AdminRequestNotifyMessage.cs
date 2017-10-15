using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("F96E7E59-D0E8-45c8-AE56-0E448C15FD96")]
	[Serializable]
	public class AdminRequestNotifyMessage
	{
		public string NotifyText
		{
			get
			{
				return this.notifytext;
			}
		}

		public AdminRequestNotifyMessage()
		{
		}

		public AdminRequestNotifyMessage(string t)
		{
			this.notifytext = t;
		}

		public override string ToString()
		{
			return "AdminRequestNotifyMessage[]";
		}

		private string notifytext = "";
	}
}
