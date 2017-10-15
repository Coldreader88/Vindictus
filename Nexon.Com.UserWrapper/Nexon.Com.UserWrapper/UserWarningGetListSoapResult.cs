using System;
using System.Collections.Generic;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserWarningGetListSoapResult : SoapResultBase
	{
		public List<UserWarningInfo> UserWarningList { get; internal set; }

		public UserWarningGetListSoapResult()
		{
			this.UserWarningList = new List<UserWarningInfo>();
		}

		public void AddUserWarning(string nexonID, DateTime createDate, DateTime lastModifyDate, byte totalWarningPoint, DateTime endWarningDate, string backOfficeUser)
		{
			if (this.UserWarningList == null)
			{
				this.UserWarningList = new List<UserWarningInfo>();
			}
			this.UserWarningList.Add(new UserWarningInfo(nexonID, createDate, lastModifyDate, totalWarningPoint, endWarningDate, backOfficeUser));
		}
	}
}
