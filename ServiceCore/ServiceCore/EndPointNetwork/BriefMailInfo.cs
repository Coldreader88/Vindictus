using System;
using System.Collections.Generic;
using Utility;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BriefMailInfo
	{
		public long MailID { get; set; }

		public byte MailType { get; set; }

		public string FromName { get; set; }

		public string ToName { get; set; }

		public string Title { get; set; }

		public int ExpireDate { get; set; }

		public bool IsRead { get; set; }

		public byte ItemAttached { get; set; }

		public int ChargedGold { get; set; }

		public ICollection<MailItemInfo> AttachedItemInfo { get; set; }

		public BriefMailInfo(long MailID, byte MailType, string FromName, string ToName, string Title, DateTime? ExpireDate, bool IsRead, BriefMailInfo.ItemAttachStatus itemAttached, int ChargedGold, ICollection<MailItemInfo> attachedItemInfo)
		{
			this.MailID = MailID;
			this.MailType = MailType;
			this.FromName = FromName;
			this.ToName = ToName;
			this.Title = Title;
			this.ExpireDate = ((ExpireDate == null) ? -1 : (ExpireDate.Value.ToInt32() - DateTime.UtcNow.ToInt32()));
			this.IsRead = IsRead;
			this.ItemAttached = (byte)itemAttached;
			this.ChargedGold = ChargedGold;
			this.AttachedItemInfo = attachedItemInfo;
		}

		public BriefMailInfo(long MailID, byte MailType, string FromName, string ToName, string Title, DateTime? ExpireDate, bool IsRead, BriefMailInfo.ItemAttachStatus itemAttached, int ChargedGold)
		{
			this.MailID = MailID;
			this.MailType = MailType;
			this.FromName = FromName;
			this.ToName = ToName;
			this.Title = Title;
			this.ExpireDate = ((ExpireDate == null) ? -1 : (ExpireDate.Value.ToInt32() - DateTime.UtcNow.ToInt32()));
			this.IsRead = IsRead;
			this.ItemAttached = (byte)itemAttached;
			this.ChargedGold = ChargedGold;
			this.AttachedItemInfo = new List<MailItemInfo>();
		}

		public override string ToString()
		{
			return string.Format("BriefMailInfo(MailID = {0} MailType = {1} FromName = {2} Title = {3} ExpireDate = {4})", new object[]
			{
				this.MailID,
				this.MailType,
				this.FromName,
				this.Title,
				this.ExpireDate
			});
		}

		public enum ItemAttachStatus
		{
			None,
			HasItem,
			Received
		}
	}
}
