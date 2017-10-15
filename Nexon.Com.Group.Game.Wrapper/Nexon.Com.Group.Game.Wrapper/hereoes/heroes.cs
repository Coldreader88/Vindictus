using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Nexon.Com.Group.Game.Wrapper.Properties;

namespace Nexon.Com.Group.Game.Wrapper.hereoes
{
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[WebServiceBinding(Name = "heroesSoap", Namespace = "http://api.guild.nexon.com/soap/")]
	[DebuggerStepThrough]
	public class heroes : SoapHttpClientProtocol
	{
		public heroes()
		{
			this.Url = Settings.Default.Nexon_Com_Group_Game_Wrapper_hereoes_heroes;
			if (this.IsLocalFileSystemWebService(this.Url))
			{
				this.UseDefaultCredentials = true;
				this.useDefaultCredentialsSetExplicitly = false;
			}
			else
			{
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

		public event GuildPRGetListCompletedEventHandler GuildPRGetListCompleted;

		public event GuildPRGetInfoCompletedEventHandler GuildPRGetInfoCompleted;

		public event GuildStoryGetListCompletedEventHandler GuildStoryGetListCompleted;

		public event GuildStoryGetInfoCompletedEventHandler GuildStoryGetInfoCompleted;

		public event GuildSearchGetListCompletedEventHandler GuildSearchGetListCompleted;

		public event GetGuildInfoCompletedEventHandler GetGuildInfoCompleted;

		public event GetGuildLatestArticleListCompletedEventHandler GetGuildLatestArticleListCompleted;

		public event GetMonthlyWebRankingListCompletedEventHandler GetMonthlyWebRankingListCompleted;

		public event GetWeeklyGameRankingListCompletedEventHandler GetWeeklyGameRankingListCompleted;

		public event RefreshNMLinkCompletedEventHandler RefreshNMLinkCompleted;

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/GuildPRGetList", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GuildPRGetList(int n4PageNo, byte n1PageSize, string strWriterName_search, string strArticleTitle_search, byte n1ArticleCategorySN, out DataSet ds, out int n4TotalRowCount)
		{
			object[] array = base.Invoke("GuildPRGetList", new object[]
			{
				n4PageNo,
				n1PageSize,
				strWriterName_search,
				strArticleTitle_search,
				n1ArticleCategorySN
			});
			ds = (DataSet)array[1];
			n4TotalRowCount = (int)array[2];
			return (int)array[0];
		}

		public void GuildPRGetListAsync(int n4PageNo, byte n1PageSize, string strWriterName_search, string strArticleTitle_search, byte n1ArticleCategorySN)
		{
			this.GuildPRGetListAsync(n4PageNo, n1PageSize, strWriterName_search, strArticleTitle_search, n1ArticleCategorySN, null);
		}

		public void GuildPRGetListAsync(int n4PageNo, byte n1PageSize, string strWriterName_search, string strArticleTitle_search, byte n1ArticleCategorySN, object userState)
		{
			if (this.GuildPRGetListOperationCompleted == null)
			{
				this.GuildPRGetListOperationCompleted = new SendOrPostCallback(this.OnGuildPRGetListOperationCompleted);
			}
			base.InvokeAsync("GuildPRGetList", new object[]
			{
				n4PageNo,
				n1PageSize,
				strWriterName_search,
				strArticleTitle_search,
				n1ArticleCategorySN
			}, this.GuildPRGetListOperationCompleted, userState);
		}

		private void OnGuildPRGetListOperationCompleted(object arg)
		{
			if (this.GuildPRGetListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GuildPRGetListCompleted(this, new GuildPRGetListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/GuildPRGetInfo", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GuildPRGetInfo(int n4ArticleSN, int n4NexonSN, out DataSet ds)
		{
			object[] array = base.Invoke("GuildPRGetInfo", new object[]
			{
				n4ArticleSN,
				n4NexonSN
			});
			ds = (DataSet)array[1];
			return (int)array[0];
		}

		public void GuildPRGetInfoAsync(int n4ArticleSN, int n4NexonSN)
		{
			this.GuildPRGetInfoAsync(n4ArticleSN, n4NexonSN, null);
		}

		public void GuildPRGetInfoAsync(int n4ArticleSN, int n4NexonSN, object userState)
		{
			if (this.GuildPRGetInfoOperationCompleted == null)
			{
				this.GuildPRGetInfoOperationCompleted = new SendOrPostCallback(this.OnGuildPRGetInfoOperationCompleted);
			}
			base.InvokeAsync("GuildPRGetInfo", new object[]
			{
				n4ArticleSN,
				n4NexonSN
			}, this.GuildPRGetInfoOperationCompleted, userState);
		}

		private void OnGuildPRGetInfoOperationCompleted(object arg)
		{
			if (this.GuildPRGetInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GuildPRGetInfoCompleted(this, new GuildPRGetInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/GuildStoryGetList", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GuildStoryGetList(int n4PageNo, byte n1PageSize, string strWriterName_search, string strArticleTitle_search, out DataSet ds, out int n4TotalRowCount)
		{
			object[] array = base.Invoke("GuildStoryGetList", new object[]
			{
				n4PageNo,
				n1PageSize,
				strWriterName_search,
				strArticleTitle_search
			});
			ds = (DataSet)array[1];
			n4TotalRowCount = (int)array[2];
			return (int)array[0];
		}

		public void GuildStoryGetListAsync(int n4PageNo, byte n1PageSize, string strWriterName_search, string strArticleTitle_search)
		{
			this.GuildStoryGetListAsync(n4PageNo, n1PageSize, strWriterName_search, strArticleTitle_search, null);
		}

		public void GuildStoryGetListAsync(int n4PageNo, byte n1PageSize, string strWriterName_search, string strArticleTitle_search, object userState)
		{
			if (this.GuildStoryGetListOperationCompleted == null)
			{
				this.GuildStoryGetListOperationCompleted = new SendOrPostCallback(this.OnGuildStoryGetListOperationCompleted);
			}
			base.InvokeAsync("GuildStoryGetList", new object[]
			{
				n4PageNo,
				n1PageSize,
				strWriterName_search,
				strArticleTitle_search
			}, this.GuildStoryGetListOperationCompleted, userState);
		}

		private void OnGuildStoryGetListOperationCompleted(object arg)
		{
			if (this.GuildStoryGetListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GuildStoryGetListCompleted(this, new GuildStoryGetListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/GuildStoryGetInfo", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GuildStoryGetInfo(int n4ArticleSN, int n4NexonSN, out DataSet ds)
		{
			object[] array = base.Invoke("GuildStoryGetInfo", new object[]
			{
				n4ArticleSN,
				n4NexonSN
			});
			ds = (DataSet)array[1];
			return (int)array[0];
		}

		public void GuildStoryGetInfoAsync(int n4ArticleSN, int n4NexonSN)
		{
			this.GuildStoryGetInfoAsync(n4ArticleSN, n4NexonSN, null);
		}

		public void GuildStoryGetInfoAsync(int n4ArticleSN, int n4NexonSN, object userState)
		{
			if (this.GuildStoryGetInfoOperationCompleted == null)
			{
				this.GuildStoryGetInfoOperationCompleted = new SendOrPostCallback(this.OnGuildStoryGetInfoOperationCompleted);
			}
			base.InvokeAsync("GuildStoryGetInfo", new object[]
			{
				n4ArticleSN,
				n4NexonSN
			}, this.GuildStoryGetInfoOperationCompleted, userState);
		}

		private void OnGuildStoryGetInfoOperationCompleted(object arg)
		{
			if (this.GuildStoryGetInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GuildStoryGetInfoCompleted(this, new GuildStoryGetInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/GuildSearchGetList", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GuildSearchGetList(int maskGameCode_group, int n4PageNo, byte n1PageSize, string strName_search, byte n1OrderingTypeCode, byte n1SearchTypeCode, out DataSet ds, out int n4TotalRowCount)
		{
			object[] array = base.Invoke("GuildSearchGetList", new object[]
			{
				maskGameCode_group,
				n4PageNo,
				n1PageSize,
				strName_search,
				n1OrderingTypeCode,
				n1SearchTypeCode
			});
			ds = (DataSet)array[1];
			n4TotalRowCount = (int)array[2];
			return (int)array[0];
		}

		public void GuildSearchGetListAsync(int maskGameCode_group, int n4PageNo, byte n1PageSize, string strName_search, byte n1OrderingTypeCode, byte n1SearchTypeCode)
		{
			this.GuildSearchGetListAsync(maskGameCode_group, n4PageNo, n1PageSize, strName_search, n1OrderingTypeCode, n1SearchTypeCode, null);
		}

		public void GuildSearchGetListAsync(int maskGameCode_group, int n4PageNo, byte n1PageSize, string strName_search, byte n1OrderingTypeCode, byte n1SearchTypeCode, object userState)
		{
			if (this.GuildSearchGetListOperationCompleted == null)
			{
				this.GuildSearchGetListOperationCompleted = new SendOrPostCallback(this.OnGuildSearchGetListOperationCompleted);
			}
			base.InvokeAsync("GuildSearchGetList", new object[]
			{
				maskGameCode_group,
				n4PageNo,
				n1PageSize,
				strName_search,
				n1OrderingTypeCode,
				n1SearchTypeCode
			}, this.GuildSearchGetListOperationCompleted, userState);
		}

		private void OnGuildSearchGetListOperationCompleted(object arg)
		{
			if (this.GuildSearchGetListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GuildSearchGetListCompleted(this, new GuildSearchGetListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/GetGuildInfo", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetGuildInfo(int maskGameCode_group, int n4ServerCode, string strCharacterName, int n4NexonSN, out DataSet ds, out short n2GameLevel, out short n2WebLevel, out int n4TodayVisitingCount, out int n4TodayArticleCount)
		{
			object[] array = base.Invoke("GetGuildInfo", new object[]
			{
				maskGameCode_group,
				n4ServerCode,
				strCharacterName,
				n4NexonSN
			});
			ds = (DataSet)array[1];
			n2GameLevel = (short)array[2];
			n2WebLevel = (short)array[3];
			n4TodayVisitingCount = (int)array[4];
			n4TodayArticleCount = (int)array[5];
			return (int)array[0];
		}

		public void GetGuildInfoAsync(int maskGameCode_group, int n4ServerCode, string strCharacterName, int n4NexonSN)
		{
			this.GetGuildInfoAsync(maskGameCode_group, n4ServerCode, strCharacterName, n4NexonSN, null);
		}

		public void GetGuildInfoAsync(int maskGameCode_group, int n4ServerCode, string strCharacterName, int n4NexonSN, object userState)
		{
			if (this.GetGuildInfoOperationCompleted == null)
			{
				this.GetGuildInfoOperationCompleted = new SendOrPostCallback(this.OnGetGuildInfoOperationCompleted);
			}
			base.InvokeAsync("GetGuildInfo", new object[]
			{
				maskGameCode_group,
				n4ServerCode,
				strCharacterName,
				n4NexonSN
			}, this.GetGuildInfoOperationCompleted, userState);
		}

		private void OnGetGuildInfoOperationCompleted(object arg)
		{
			if (this.GetGuildInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetGuildInfoCompleted(this, new GetGuildInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/GetGuildLatestArticleList", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetGuildLatestArticleList(int maskGameCode_group, int oidUser_group, out DataSet ds)
		{
			object[] array = base.Invoke("GetGuildLatestArticleList", new object[]
			{
				maskGameCode_group,
				oidUser_group
			});
			ds = (DataSet)array[1];
			return (int)array[0];
		}

		public void GetGuildLatestArticleListAsync(int maskGameCode_group, int oidUser_group)
		{
			this.GetGuildLatestArticleListAsync(maskGameCode_group, oidUser_group, null);
		}

		public void GetGuildLatestArticleListAsync(int maskGameCode_group, int oidUser_group, object userState)
		{
			if (this.GetGuildLatestArticleListOperationCompleted == null)
			{
				this.GetGuildLatestArticleListOperationCompleted = new SendOrPostCallback(this.OnGetGuildLatestArticleListOperationCompleted);
			}
			base.InvokeAsync("GetGuildLatestArticleList", new object[]
			{
				maskGameCode_group,
				oidUser_group
			}, this.GetGuildLatestArticleListOperationCompleted, userState);
		}

		private void OnGetGuildLatestArticleListOperationCompleted(object arg)
		{
			if (this.GetGuildLatestArticleListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetGuildLatestArticleListCompleted(this, new GetGuildLatestArticleListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/GetMonthlyWebRankingList", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetMonthlyWebRankingList(int maskGameCode_group, out DataSet ds)
		{
			object[] array = base.Invoke("GetMonthlyWebRankingList", new object[]
			{
				maskGameCode_group
			});
			ds = (DataSet)array[1];
			return (int)array[0];
		}

		public void GetMonthlyWebRankingListAsync(int maskGameCode_group)
		{
			this.GetMonthlyWebRankingListAsync(maskGameCode_group, null);
		}

		public void GetMonthlyWebRankingListAsync(int maskGameCode_group, object userState)
		{
			if (this.GetMonthlyWebRankingListOperationCompleted == null)
			{
				this.GetMonthlyWebRankingListOperationCompleted = new SendOrPostCallback(this.OnGetMonthlyWebRankingListOperationCompleted);
			}
			base.InvokeAsync("GetMonthlyWebRankingList", new object[]
			{
				maskGameCode_group
			}, this.GetMonthlyWebRankingListOperationCompleted, userState);
		}

		private void OnGetMonthlyWebRankingListOperationCompleted(object arg)
		{
			if (this.GetMonthlyWebRankingListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMonthlyWebRankingListCompleted(this, new GetMonthlyWebRankingListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/GetWeeklyGameRankingList", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetWeeklyGameRankingList(int maskGameCode_group, out DataSet ds)
		{
			object[] array = base.Invoke("GetWeeklyGameRankingList", new object[]
			{
				maskGameCode_group
			});
			ds = (DataSet)array[1];
			return (int)array[0];
		}

		public void GetWeeklyGameRankingListAsync(int maskGameCode_group)
		{
			this.GetWeeklyGameRankingListAsync(maskGameCode_group, null);
		}

		public void GetWeeklyGameRankingListAsync(int maskGameCode_group, object userState)
		{
			if (this.GetWeeklyGameRankingListOperationCompleted == null)
			{
				this.GetWeeklyGameRankingListOperationCompleted = new SendOrPostCallback(this.OnGetWeeklyGameRankingListOperationCompleted);
			}
			base.InvokeAsync("GetWeeklyGameRankingList", new object[]
			{
				maskGameCode_group
			}, this.GetWeeklyGameRankingListOperationCompleted, userState);
		}

		private void OnGetWeeklyGameRankingListOperationCompleted(object arg)
		{
			if (this.GetWeeklyGameRankingListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetWeeklyGameRankingListCompleted(this, new GetWeeklyGameRankingListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.guild.nexon.com/soap/RefreshNMLink", RequestNamespace = "http://api.guild.nexon.com/soap/", ResponseNamespace = "http://api.guild.nexon.com/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int RefreshNMLink(int NexonSN)
		{
			object[] array = base.Invoke("RefreshNMLink", new object[]
			{
				NexonSN
			});
			return (int)array[0];
		}

		public void RefreshNMLinkAsync(int NexonSN)
		{
			this.RefreshNMLinkAsync(NexonSN, null);
		}

		public void RefreshNMLinkAsync(int NexonSN, object userState)
		{
			if (this.RefreshNMLinkOperationCompleted == null)
			{
				this.RefreshNMLinkOperationCompleted = new SendOrPostCallback(this.OnRefreshNMLinkOperationCompleted);
			}
			base.InvokeAsync("RefreshNMLink", new object[]
			{
				NexonSN
			}, this.RefreshNMLinkOperationCompleted, userState);
		}

		private void OnRefreshNMLinkOperationCompleted(object arg)
		{
			if (this.RefreshNMLinkCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RefreshNMLinkCompleted(this, new RefreshNMLinkCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			bool result;
			if (url == null || url == string.Empty)
			{
				result = false;
			}
			else
			{
				Uri uri = new Uri(url);
				result = (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0);
			}
			return result;
		}

		private SendOrPostCallback GuildPRGetListOperationCompleted;

		private SendOrPostCallback GuildPRGetInfoOperationCompleted;

		private SendOrPostCallback GuildStoryGetListOperationCompleted;

		private SendOrPostCallback GuildStoryGetInfoOperationCompleted;

		private SendOrPostCallback GuildSearchGetListOperationCompleted;

		private SendOrPostCallback GetGuildInfoOperationCompleted;

		private SendOrPostCallback GetGuildLatestArticleListOperationCompleted;

		private SendOrPostCallback GetMonthlyWebRankingListOperationCompleted;

		private SendOrPostCallback GetWeeklyGameRankingListOperationCompleted;

		private SendOrPostCallback RefreshNMLinkOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
