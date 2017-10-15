using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ReturnMailMessage : IMessage
	{
		public long MailID { get; set; }

		public ReturnMailMessage(long MailID)
		{
			this.MailID = MailID;
		}

		public override string ToString()
		{
			return string.Format("ReturnMailMessage[ MailID = {0} ]", this.MailID);
		}
	}
}
