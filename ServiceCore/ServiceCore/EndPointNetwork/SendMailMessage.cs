using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SendMailMessage : IMessage
	{
		public byte MailType { get; set; }

		public string ToName { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public ICollection<AttachedTransferItemInfo> ItemList { get; set; }

		public int Gold { get; set; }

		public bool Express { get; set; }

		public int ChargedGold { get; set; }

		public SendMailMessage(byte MailType, string ToName, string Title, string Content, ICollection<AttachedTransferItemInfo> ItemList, int Gold, bool Express, int ChargedGold)
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

		public override string ToString()
		{
			string text = string.Format("SendMailMessage[ MailType = {0} ToName = {1} Title = {2} Contents = {3} Gold = {4} Express = {5} ChargedGold = {6} ", new object[]
			{
				this.MailType,
				this.ToName,
				this.Title,
				this.Content,
				this.Gold,
				this.Express,
				this.ChargedGold
			});
			if (this.ItemList.Count > 0)
			{
				text += "Items = {";
				bool flag = true;
				foreach (AttachedTransferItemInfo attachedTransferItemInfo in this.ItemList)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						text += ", ";
					}
					text += string.Format("{0}({1})x{2}", attachedTransferItemInfo.ItemClass, attachedTransferItemInfo.ItemID, attachedTransferItemInfo.ItemCount);
				}
				text += "}";
			}
			text += "]";
			return text;
		}
	}
}
