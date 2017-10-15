using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Nexon.Com.Group.Game.Wrapper.hereoes
{
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	public class GuildPRGetInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GuildPRGetInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public DataSet ds
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSet)this.results[1];
			}
		}

		private object[] results;
	}
}
