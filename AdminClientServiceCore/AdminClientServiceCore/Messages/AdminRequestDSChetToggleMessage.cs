using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("D1A8EE50-FCAA-43d2-A1F9-7E85FF0C77AE")]
	[Serializable]
	public class AdminRequestDSChetToggleMessage
	{
		public int On
		{
			get
			{
				return this.ison;
			}
		}

		public AdminRequestDSChetToggleMessage()
		{
		}

		public AdminRequestDSChetToggleMessage(int on)
		{
			this.ison = on;
		}

		public override string ToString()
		{
			return string.Format("AdminRequestDSChetToggleMessage[{0}]", this.ison);
		}

		private int ison;
	}
}
