using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CheckSessionAndGetInfo4CompletedEventArgs : AsyncCompletedEventArgs
	{
		internal CheckSessionAndGetInfo4CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public CheckSessionAndGetInfo4Result Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CheckSessionAndGetInfo4Result)this.results[0];
			}
		}

		private object[] results;
	}
}
