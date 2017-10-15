using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class ReturnMail : Operation
	{
		public long MailID { get; set; }

		public ReturnMail(long MailID)
		{
			this.MailID = MailID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
