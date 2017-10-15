using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DesignerCategory("code")]
	public class CheckSessionAndGetInfo2CompletedEventArgs : AsyncCompletedEventArgs
	{
		internal CheckSessionAndGetInfo2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public CheckSessionAndGetInfo2Result Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CheckSessionAndGetInfo2Result)this.results[0];
			}
		}

		private object[] results;
	}
}
