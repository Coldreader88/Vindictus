using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MailListMessage : IMessage
	{
		public ICollection<BriefMailInfo> ReceivedMailList { get; set; }

		public ICollection<BriefMailInfo> SentMailList { get; set; }

		public int NewMailCount { get; set; }

		public MailListMessage(ICollection<BriefMailInfo> receivedMailList, ICollection<BriefMailInfo> sentMailList, int newMailCount)
		{
			this.ReceivedMailList = receivedMailList;
			this.SentMailList = sentMailList;
			this.NewMailCount = newMailCount;
		}

		public override string ToString()
		{
			return string.Format("MailListMessage[ BriefMailInfo x {0} ]", this.ReceivedMailList.Count);
		}
	}
}
