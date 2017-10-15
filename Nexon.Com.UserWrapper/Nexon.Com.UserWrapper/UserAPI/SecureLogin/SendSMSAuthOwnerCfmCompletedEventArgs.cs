using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.SecureLogin
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SendSMSAuthOwnerCfmCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal SendSMSAuthOwnerCfmCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public long n8AuthLogSN
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (long)this.results[1];
			}
		}

		public byte n1AuthPGCode
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[2];
			}
		}

		public string strPccSeq
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[3];
			}
		}

		public string strAuthSeq
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[4];
			}
		}

		public string strErrorMessage
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[5];
			}
		}

		private object[] results;
	}
}
