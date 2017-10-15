using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace NPL.SSO.com.nexon.auth
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class PCBangLoginCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal PCBangLoginCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public PCBangLoginResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (PCBangLoginResult)this.results[0];
			}
		}

		private object[] results;
	}
}
