using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MailInfoMessage : IMessage
	{
		public long MailID { get; set; }

		public ICollection<MailItemInfo> Items { get; set; }

		public byte MailType { get; set; }

		public string FromName { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public int ChargedGold { get; set; }

		public MailInfoMessage(long mailID, byte mailType, string fromName, string title, string content, ICollection<MailItemInfo> items, int chargedGold)
		{
			this.MailID = mailID;
			this.MailType = mailType;
			this.FromName = fromName;
			this.Title = title;
			this.Content = content;
			this.Items = items;
			this.ChargedGold = chargedGold;
		}

		public override string ToString()
		{
			return string.Format("MailInfoMessage[ MailID = {0} MailType = {1} FromName = {2} Title = {3} Content = {4} Item x {5} ]", new object[]
			{
				this.MailID,
				this.MailType,
				this.FromName,
				this.Title,
				this.Content,
				this.Items.Count
			});
		}
	}
}
