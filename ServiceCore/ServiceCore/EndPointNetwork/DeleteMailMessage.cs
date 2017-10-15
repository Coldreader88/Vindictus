using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DeleteMailMessage : IMessage
	{
		public ICollection<long> MailList { get; set; }

		public DeleteMailMessage(ICollection<long> mailList)
		{
			this.MailList = mailList;
		}

		public override string ToString()
		{
			return string.Format("DeleteMailMessage[ ID x {0} ]", this.MailList.Count);
		}
	}
}
