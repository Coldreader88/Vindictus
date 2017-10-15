using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class GetMailItem : Operation
	{
		public long MailID { get; set; }

		public bool GetChargedMailItemAllowed { get; set; }

		public GetMailItemCompletedMessage.ResultEnum ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		public GetMailItem(long MailID, bool GetChargedMailItemAllowed)
		{
			this.MailID = MailID;
			this.GetChargedMailItemAllowed = GetChargedMailItemAllowed;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GetMailItem.Request(this);
		}

		[NonSerialized]
		private GetMailItemCompletedMessage.ResultEnum resultCode;

		private class Request : OperationProcessor<GetMailItem>
		{
			public Request(GetMailItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.resultCode = (GetMailItemCompletedMessage.ResultEnum)base.Feedback;
				yield break;
			}
		}
	}
}
