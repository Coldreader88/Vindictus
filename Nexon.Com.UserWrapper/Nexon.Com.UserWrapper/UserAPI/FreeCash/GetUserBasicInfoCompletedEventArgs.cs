using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.FreeCash
{
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
	[DebuggerStepThrough]
	public class GetUserBasicInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GetUserBasicInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public bool isHasSsn
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		public byte n1Age
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[2];
			}
		}

		public bool isTempUser_child
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[3];
			}
		}

		private object[] results;
	}
}
