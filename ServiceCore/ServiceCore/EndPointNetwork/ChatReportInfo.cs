using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ChatReportInfo
	{
		public string name { get; set; }

		public string msg { get; set; }

		public ChatReportInfo(string name, string msg)
		{
			this.name = name;
			this.msg = msg;
		}
	}
}
