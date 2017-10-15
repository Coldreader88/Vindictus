using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Nexon.Com.Group.Game.Wrapper.hereoes
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	public class GuildSearchGetListCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GuildSearchGetListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public int n4TotalRowCount
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[2];
			}
		}

		private object[] results;
	}
}
