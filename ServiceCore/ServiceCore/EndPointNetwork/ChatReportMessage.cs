using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ChatReportMessage : IMessage
	{
		public string m_Name { get; set; }

		public int m_Type { get; set; }

		public string m_Reason { get; set; }

		public string m_ChatLog { get; set; }

		public override string ToString()
		{
			return string.Format("ChatReportMessage [{0}, {1}]", this.m_Name, this.m_Type);
		}
	}
}
