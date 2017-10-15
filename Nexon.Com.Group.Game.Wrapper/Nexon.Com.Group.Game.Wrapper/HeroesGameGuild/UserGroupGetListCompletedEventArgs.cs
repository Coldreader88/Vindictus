using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Nexon.Com.Group.Game.Wrapper.HeroesGameGuild
{
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	public class UserGroupGetListCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal UserGroupGetListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

		public UserGroupInfo[] UserGroupList
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UserGroupInfo[])this.results[2];
			}
		}

		public int RowCount
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[3];
			}
		}

		public int TotalRowCount
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[4];
			}
		}

		private object[] results;
	}
}
