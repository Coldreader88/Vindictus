using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	public class UpgradeGameTokenCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal UpgradeGameTokenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public UpgradeGameTokenResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpgradeGameTokenResult)this.results[0];
			}
		}

		private object[] results;
	}
}
