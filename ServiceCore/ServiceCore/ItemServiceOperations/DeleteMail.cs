using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class DeleteMail : Operation
	{
		public ICollection<long> MailList { get; set; }

		public DeleteMail(ICollection<long> mailList)
		{
			this.MailList = mailList;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
