using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Nexon.Com.Group.Game.Wrapper.HeroesGameGuild
{
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	public class GroupGetListByGuildMasterCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GroupGetListByGuildMasterCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public string strErrorMessage
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		public GroupInfo[] GroupList
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GroupInfo[])this.results[2];
			}
		}

		public int TotalRowCount
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
