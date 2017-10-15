using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.Warning
{
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	[DesignerCategory("code")]
	public class GetWarningLogListCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GetWarningLogListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		public int n4TotalRowCount
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[1];
			}
		}

		public DataSet dsWarningLogList
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSet)this.results[2];
			}
		}

		private object[] results;
	}
}
