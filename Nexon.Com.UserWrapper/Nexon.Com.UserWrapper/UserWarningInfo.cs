using System;

namespace Nexon.Com.UserWrapper
{
	public class UserWarningInfo
	{
		public UserWarningInfo()
		{
		}

		public UserWarningInfo(string nexonID, DateTime createDate, DateTime lastModifyDate, byte totalWarningPoint, DateTime endWarningDate, string backOfficeUser)
		{
			this.NexonID = nexonID;
			this.CreateDate = createDate;
			this.LastModifyDate = lastModifyDate;
			this.TotalWarningPoint = totalWarningPoint;
			this.EndWarningDate = endWarningDate;
			this.BackOfficeUser = backOfficeUser;
		}

		public string NexonID { get; internal set; }

		public DateTime CreateDate { get; internal set; }

		public DateTime LastModifyDate { get; internal set; }

		public byte TotalWarningPoint { get; internal set; }

		public DateTime EndWarningDate { get; internal set; }

		public string BackOfficeUser { get; internal set; }
	}
}
