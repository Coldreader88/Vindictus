using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	public class CheckAndAttachSessionCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal CheckAndAttachSessionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public CheckAndAttachSessionResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CheckAndAttachSessionResult)this.results[0];
			}
		}

		private object[] results;
	}
}
