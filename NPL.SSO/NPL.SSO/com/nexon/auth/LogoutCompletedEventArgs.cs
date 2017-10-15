using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class LogoutCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal LogoutCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public LogoutResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (LogoutResult)this.results[0];
			}
		}

		private object[] results;
	}
}
