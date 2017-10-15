using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class SendMail : Operation
	{
		public MailType MailType { get; set; }

		public string ToName { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public ICollection<TransferItemInfo> ItemList { get; set; }

		public int Gold { get; set; }

		public bool Express { get; set; }

		public int ChargedGold { get; set; }

		public MailSentMessage.ErrorCodeEnum ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		public SendMail(MailType MailType, string ToName, string Title, string Content, long ItemID, string ItemClass, int ItemCount, int Gold, bool Express, int ChargedGold)
		{
			this.MailType = MailType;
			this.ToName = ToName;
			this.Title = Title;
			this.Content = Content;
			this.ItemList = new List<TransferItemInfo>
			{
				new TransferItemInfo(ItemID, ItemClass, ItemCount)
			};
			this.Gold = Gold;
			this.Express = Express;
			this.ChargedGold = ChargedGold;
		}

		public SendMail(MailType MailType, string ToName, string Title, string Content, ICollection<TransferItemInfo> ItemList, int Gold, bool Express, int ChargedGold)
		{
			this.MailType = MailType;
			this.ToName = ToName;
			this.Title = Title;
			this.Content = Content;
			this.ItemList = ItemList;
			this.Gold = Gold;
			this.Express = Express;
			this.ChargedGold = ChargedGold;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new SendMail.Request(this);
		}

		[NonSerialized]
		private MailSentMessage.ErrorCodeEnum resultCode;

		private class Request : OperationProcessor<SendMail>
		{
			public Request(SendMail op) : base(op)
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
