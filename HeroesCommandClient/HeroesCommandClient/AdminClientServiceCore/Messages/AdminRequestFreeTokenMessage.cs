using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("F271BDD9-2792-4bb7-B956-F4C56AA6657B")]
	[Serializable]
	public class AdminRequestFreeTokenMessage
	{
		public bool On
		{
			get
			{
				return this.ison;
			}
		}

		public AdminRequestFreeTokenMessage()
		{
		}

		public AdminRequestFreeTokenMessage(bool on)
		{
			this.ison = on;
		}

		public override string ToString()
		{
			return string.Format("AdminRequestFreeTokenMessage[{0}]", this.ison);
		}

		private bool ison;
	}
}
