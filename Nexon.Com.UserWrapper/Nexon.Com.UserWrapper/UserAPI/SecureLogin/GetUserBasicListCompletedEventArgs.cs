using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.SecureLogin
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUserBasicListCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GetUserBasicListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public DataSet ndsList
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSet)this.results[1];
			}
		}

		public int n4TotalRowCount
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[2];
			}
		}

		public bool isAdminUser
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[3];
			}
		}

		private object[] results;
	}
}
