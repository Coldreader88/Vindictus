using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("1E0476E6-558D-448b-B02F-FE6FD22DF392")]
	[Serializable]
	public class AdminRequestKickMessage
	{
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		public bool IsUID
		{
			get
			{
				return this.b;
			}
		}

		public AdminRequestKickMessage()
		{
			this.id = "";
			this.b = false;
		}

		public AdminRequestKickMessage(string IDString, bool isUID)
		{
			this.id = IDString;
			this.b = isUID;
		}

		public override string ToString()
		{
			return string.Format("AdminRequestKickMessage [{0}/{1}]", this.id, this.b ? "UID" : "CID");
		}

		private bool b;

		private string id = "";
	}
}
