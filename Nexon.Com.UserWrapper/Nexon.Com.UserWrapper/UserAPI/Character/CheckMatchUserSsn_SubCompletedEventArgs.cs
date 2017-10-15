using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.Character
{
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CheckMatchUserSsn_SubCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal CheckMatchUserSsn_SubCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		private object[] results;
	}
}
