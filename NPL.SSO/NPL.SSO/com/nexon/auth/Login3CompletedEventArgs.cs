using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DesignerCategory("code")]
	public class Login3CompletedEventArgs : AsyncCompletedEventArgs
	{
		internal Login3CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public LoginResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (LoginResult)this.results[0];
			}
		}

		private object[] results;
	}
}
