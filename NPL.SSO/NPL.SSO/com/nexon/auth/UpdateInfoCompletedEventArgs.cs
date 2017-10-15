using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class UpdateInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal UpdateInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public UpdateInfoResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateInfoResult)this.results[0];
			}
		}

		private object[] results;
	}
}
