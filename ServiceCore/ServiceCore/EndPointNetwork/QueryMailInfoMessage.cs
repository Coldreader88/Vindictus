using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryMailInfoMessage : IMessage
	{
		public long MailID { get; set; }

		public QueryMailInfoMessage(long MailID)
		{
			this.MailID = MailID;
		}

		public override string ToString()
		{
			return string.Format("QueryMailInfoMessage[ MailID = {0} ]", this.MailID);
		}
	}
}
