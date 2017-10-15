using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.P2
{
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DesignerCategory("code")]
	public class GetUserWriteStatusCodeCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GetUserWriteStatusCodeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public byte n1WriteStatusCode
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[1];
			}
		}

		private object[] results;
	}
}
