using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.GuestUser
{
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	[DesignerCategory("code")]
	public class CheckEnableChangeUserCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal CheckEnableChangeUserCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public int n4NexonSN
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[1];
			}
		}

		private object[] results;
	}
}
