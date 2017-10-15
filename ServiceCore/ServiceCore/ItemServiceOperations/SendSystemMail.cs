using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class SendSystemMail : Operation
	{
		public MailType MailType { get; set; }

		public long ToCID { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public ICollection<TransferItemInfo> ItemList { get; set; }

		public int Gold { get; set; }

		public MailSentMessage.ErrorCodeEnum ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		public SendSystemMail(MailType MailType, long ToCID, string Title, string Content, long ItemID, string ItemClass, int ItemCount, int Gold)
		{
			this.MailType = MailType;
			this.ToCID = ToCID;
			this.Title = Title;
			this.Content = Content;
			this.ItemList = new List<TransferItemInfo>
			{
				new TransferItemInfo(ItemID, ItemClass, ItemCount)
			};
			this.Gold = Gold;
		}

		public SendSystemMail(MailType MailType, long ToCID, string Title, string Content, ICollection<TransferItemInfo> ItemList, int Gold)
		{
			this.MailType = MailType;
			this.ToCID = ToCID;
			this.Title = Title;
			this.Content = Content;
			this.ItemList = ItemList;
			this.Gold = Gold;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new SendSystemMail.Request(this);
		}

		[NonSerialized]
		private MailSentMessage.ErrorCodeEnum resultCode;

		private class Request : OperationProcessor<SendSystemMail>
		{
			public Request(SendSystemMail op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.resultCode = (MailSentMessage.ErrorCodeEnum)base.Feedback;
				yield break;
			}
		}
	}
}
