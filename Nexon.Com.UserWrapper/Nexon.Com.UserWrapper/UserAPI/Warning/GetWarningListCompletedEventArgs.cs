using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.Warning
{
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	public class GetWarningListCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GetWarningListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public DataSet dsWarningList
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
