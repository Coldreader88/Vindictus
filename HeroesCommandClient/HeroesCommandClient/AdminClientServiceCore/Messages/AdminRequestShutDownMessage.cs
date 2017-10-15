using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("5401B3BD-ED3F-44a6-AE03-DFDA02F1EE3D")]
	[Serializable]
	public class AdminRequestShutDownMessage
	{
		public int Delay
		{
			get
			{
				return this.delay;
			}
		}

		public string Announce
		{
			get
			{
				return this.announce;
			}
		}

		public AdminRequestShutDownMessage()
		{
		}

		public AdminRequestShutDownMessage(int v, string s)
		{
			this.delay = v;
			this.announce = s;
		}

		public override string ToString()
		{
			return string.Format("AdminRequestShutDownMessage[{0}s][{1}]", this.delay, this.announce);
		}

		private int delay;

		private string announce = "";
	}
}
