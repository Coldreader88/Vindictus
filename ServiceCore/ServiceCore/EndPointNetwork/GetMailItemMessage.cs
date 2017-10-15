using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GetMailItemMessage : IMessage
	{
		public long MailID { get; set; }

		public GetMailItemMessage(long MailID)
		{
			this.MailID = MailID;
		}

		public override string ToString()
		{
			return string.Format("GetMailItemMessage[ MailID = {0} ]", this.MailID);
		}
	}
}
