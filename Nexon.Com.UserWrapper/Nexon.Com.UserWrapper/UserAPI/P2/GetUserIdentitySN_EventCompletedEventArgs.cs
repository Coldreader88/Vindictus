using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.P2
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	public class GetUserIdentitySN_EventCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GetUserIdentitySN_EventCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public long n8IdentitySN
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (long)this.results[1];
			}
		}

		public long n8IdentitySN_Recommended
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (long)this.results[2];
			}
		}

		public int n4NexonSN_Recommended
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[3];
			}
		}

		private object[] results;
	}
}
