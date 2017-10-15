using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class AuthorizedLogin64CompletedEventArgs : AsyncCompletedEventArgs
	{
		internal AuthorizedLogin64CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public AuthorizedLoginResult64 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AuthorizedLoginResult64)this.results[0];
			}
		}

		private object[] results;
	}
}
