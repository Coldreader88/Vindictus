using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class IsOTPUsableCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal IsOTPUsableCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public IsOTPUsableResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (IsOTPUsableResult)this.results[0];
			}
		}

		private object[] results;
	}
}
