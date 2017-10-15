using System;
using Nexon.Com.DAO;

namespace Nexon.Com.Log
{
	internal class ErrorLogCreateSPResult : SPResultBase
	{
		public ErrorLogInfo errorLogInfo { get; set; }

		public int n4ErrorLogSN { get; set; }

		public DateTime dtCreateDate { get; set; }
	}
}
