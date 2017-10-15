using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CheckSessionAndUpdateInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal CheckSessionAndUpdateInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public CheckSessionResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CheckSessionResult)this.results[0];
			}
		}

		private object[] results;
	}
}
