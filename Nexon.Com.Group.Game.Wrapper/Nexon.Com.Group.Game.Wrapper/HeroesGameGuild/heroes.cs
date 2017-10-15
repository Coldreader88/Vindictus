using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Nexon.Com.Group.Game.Wrapper.Properties;

namespace Nexon.Com.Group.Game.Wrapper.HeroesGameGuild
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "heroesSoap", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	public class heroes : SoapHttpClientProtocol
	{
		public heroes()
		{
			this.Url = Settings.Default.Nexon_Com_Group_Game_Wrapper_HeroesGameGuild_heroes;
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

		public event AllMemberGetListCompletedEventHandler AllMemberGetListCompleted;

		public event CheckGroupIDCompletedEventHandler CheckGroupIDCompleted;

		public event CheckGroupNameCompletedEventHandler CheckGroupNameCompleted;

		public event CreateCompletedEventHandler CreateCompleted;

		public event GroupGetInfoByGuildSNCompletedEventHandler GroupGetInfoByGuildSNCompleted;

		public event GroupGetInfoByGuildNameCompletedEventHandler GroupGetInfoByGuildNameCompleted;

		public event GroupGetListByGuildMasterCompletedEventHandler GroupGetListByGuildMasterCompleted;

		public event GroupGetListByGuildNameCompletedEventHandler GroupGetListByGuildNameCompleted;

		public event GroupMemberGetInfoCompletedEventHandler GroupMemberGetInfoCompleted;

		public event GroupUserGetInfoCompletedEventHandler GroupUserGetInfoCompleted;

		public event RemoveCompletedEventHandler RemoveCompleted;

		public event UserGroupGetListCompletedEventHandler UserGroupGetListCompleted;

		public event UserJoinCompletedEventHandler UserJoinCompleted;

		public event UserJoinApplyCompletedEventHandler UserJoinApplyCompleted;

		public event UserJoinRejectCompletedEventHandler UserJoinRejectCompleted;

		public event UserSecedeCompletedEventHandler UserSecedeCompleted;

		public event UserTypeModifyCompletedEventHandler UserTypeModifyCompleted;

		public event MemberLoginCompletedEventHandler MemberLoginCompleted;

		public event GroupUserTryJoinCompletedEventHandler GroupUserTryJoinCompleted;

		public event GroupChangeMasterCompletedEventHandler GroupChangeMasterCompleted;

		[SoapDocumentMethod("http://tempuri.org/AllMemberGetList", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int AllMemberGetList(int ServerCode, int GuildSN, int PageNo, byte PageSize, bool isAscending, out string strErrorMessage, out GroupMemberInfo[] MemberList, out int RowCount, out int TotalRowCount)
		{
			object[] array = base.Invoke("AllMemberGetList", new object[]
			{
				ServerCode,
				GuildSN,
				PageNo,
				PageSize,
				isAscending
			});
			strErrorMessage = (string)array[1];
			MemberList = (GroupMemberInfo[])array[2];
			RowCount = (int)array[3];
			TotalRowCount = (int)array[4];
			return (int)array[0];
		}

		public void AllMemberGetListAsync(int ServerCode, int GuildSN, int PageNo, byte PageSize, bool isAscending)
		{
			this.AllMemberGetListAsync(ServerCode, GuildSN, PageNo, PageSize, isAscending, null);
		}

		public void AllMemberGetListAsync(int ServerCode, int GuildSN, int PageNo, byte PageSize, bool isAscending, object userState)
		{
			if (this.AllMemberGetListOperationCompleted == null)
			{
				this.AllMemberGetListOperationCompleted = new SendOrPostCallback(this.OnAllMemberGetListOperationCompleted);
			}
			base.InvokeAsync("AllMemberGetList", new object[]
			{
				ServerCode,
				GuildSN,
				PageNo,
				PageSize,
				isAscending
			}, this.AllMemberGetListOperationCompleted, userState);
		}

		private void OnAllMemberGetListOperationCompleted(object arg)
		{
			if (this.AllMemberGetListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AllMemberGetListCompleted(this, new AllMemberGetListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckGroupID", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckGroupID(int ServerCode, string GuildID, out string strErrorMessage)
		{
			object[] array = base.Invoke("CheckGroupID", new object[]
			{
				ServerCode,
				GuildID
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void CheckGroupIDAsync(int ServerCode, string GuildID)
		{
			this.CheckGroupIDAsync(ServerCode, GuildID, null);
		}

		public void CheckGroupIDAsync(int ServerCode, string GuildID, object userState)
		{
			if (this.CheckGroupIDOperationCompleted == null)
			{
				this.CheckGroupIDOperationCompleted = new SendOrPostCallback(this.OnCheckGroupIDOperationCompleted);
			}
			base.InvokeAsync("CheckGroupID", new object[]
			{
				ServerCode,
				GuildID
			}, this.CheckGroupIDOperationCompleted, userState);
		}

		private void OnCheckGroupIDOperationCompleted(object arg)
		{
			if (this.CheckGroupIDCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckGroupIDCompleted(this, new CheckGroupIDCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckGroupName", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckGroupName(int ServerCode, string GuildName, out string strErrorMessage)
		{
			object[] array = base.Invoke("CheckGroupName", new object[]
			{
				ServerCode,
				GuildName
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void CheckGroupNameAsync(int ServerCode, string GuildName)
		{
			this.CheckGroupNameAsync(ServerCode, GuildName, null);
		}

		public void CheckGroupNameAsync(int ServerCode, string GuildName, object userState)
		{
			if (this.CheckGroupNameOperationCompleted == null)
			{
				this.CheckGroupNameOperationCompleted = new SendOrPostCallback(this.OnCheckGroupNameOperationCompleted);
			}
			base.InvokeAsync("CheckGroupName", new object[]
			{
				ServerCode,
				GuildName
			}, this.CheckGroupNameOperationCompleted, userState);
		}

		private void OnCheckGroupNameOperationCompleted(object arg)
		{
			if (this.CheckGroupNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckGroupNameCompleted(this, new CheckGroupNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GroupCreate", RequestElementName = "GroupCreate", RequestNamespace = "http://tempuri.org/", ResponseElementName = "GroupCreateResponse", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("GroupCreateResult")]
		public int Create(int ServerCode, int NexonSN, long CharacterSN, string CharacterName, string GuildName, string GuildID, string GuildIntro, out int GroupSN, out string strErrorMessage)
		{
			object[] array = base.Invoke("Create", new object[]
			{
				ServerCode,
				NexonSN,
				CharacterSN,
				CharacterName,
				GuildName,
				GuildID,
				GuildIntro
			});
			GroupSN = (int)array[1];
			strErrorMessage = (string)array[2];
			return (int)array[0];
		}

		public void CreateAsync(int ServerCode, int NexonSN, long CharacterSN, string CharacterName, string GuildName, string GuildID, string GuildIntro)
		{
			this.CreateAsync(ServerCode, NexonSN, CharacterSN, CharacterName, GuildName, GuildID, GuildIntro, null);
		}

		public void CreateAsync(int ServerCode, int NexonSN, long CharacterSN, string CharacterName, string GuildName, string GuildID, string GuildIntro, object userState)
		{
			if (this.CreateOperationCompleted == null)
			{
				this.CreateOperationCompleted = new SendOrPostCallback(this.OnCreateOperationCompleted);
			}
			base.InvokeAsync("Create", new object[]
			{
				ServerCode,
				NexonSN,
				CharacterSN,
				CharacterName,
				GuildName,
				GuildID,
				GuildIntro
			}, this.CreateOperationCompleted, userState);
		}

		private void OnCreateOperationCompleted(object arg)
		{
			if (this.CreateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateCompleted(this, new CreateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GroupGetInfoByGuildSN", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GroupGetInfoByGuildSN(int ServerCode, int GuildSN, out string strErrorMessage, out GroupInfo Info)
		{
			object[] array = base.Invoke("GroupGetInfoByGuildSN", new object[]
			{
				ServerCode,
				GuildSN
			});
			strErrorMessage = (string)array[1];
			Info = (GroupInfo)array[2];
			return (int)array[0];
		}

		public void GroupGetInfoByGuildSNAsync(int ServerCode, int GuildSN)
		{
			this.GroupGetInfoByGuildSNAsync(ServerCode, GuildSN, null);
		}

		public void GroupGetInfoByGuildSNAsync(int ServerCode, int GuildSN, object userState)
		{
			if (this.GroupGetInfoByGuildSNOperationCompleted == null)
			{
				this.GroupGetInfoByGuildSNOperationCompleted = new SendOrPostCallback(this.OnGroupGetInfoByGuildSNOperationCompleted);
			}
			base.InvokeAsync("GroupGetInfoByGuildSN", new object[]
			{
				ServerCode,
				GuildSN
			}, this.GroupGetInfoByGuildSNOperationCompleted, userState);
		}

		private void OnGroupGetInfoByGuildSNOperationCompleted(object arg)
		{
			if (this.GroupGetInfoByGuildSNCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GroupGetInfoByGuildSNCompleted(this, new GroupGetInfoByGuildSNCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GroupGetInfoByGuildName", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GroupGetInfoByGuildName(int ServerCode, string GuildName, out string strErrorMessage, out GroupInfo Info)
		{
			object[] array = base.Invoke("GroupGetInfoByGuildName", new object[]
			{
				ServerCode,
				GuildName
			});
			strErrorMessage = (string)array[1];
			Info = (GroupInfo)array[2];
			return (int)array[0];
		}

		public void GroupGetInfoByGuildNameAsync(int ServerCode, string GuildName)
		{
			this.GroupGetInfoByGuildNameAsync(ServerCode, GuildName, null);
		}

		public void GroupGetInfoByGuildNameAsync(int ServerCode, string GuildName, object userState)
		{
			if (this.GroupGetInfoByGuildNameOperationCompleted == null)
			{
				this.GroupGetInfoByGuildNameOperationCompleted = new SendOrPostCallback(this.OnGroupGetInfoByGuildNameOperationCompleted);
			}
			base.InvokeAsync("GroupGetInfoByGuildName", new object[]
			{
				ServerCode,
				GuildName
			}, this.GroupGetInfoByGuildNameOperationCompleted, userState);
		}

		private void OnGroupGetInfoByGuildNameOperationCompleted(object arg)
		{
			if (this.GroupGetInfoByGuildNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GroupGetInfoByGuildNameCompleted(this, new GroupGetInfoByGuildNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GroupGetListByGuildMaster", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GroupGetListByGuildMaster(int ServerCode, byte OrderType, int PageNo, byte PageSize, string NameInGroup_Master_Search, out string strErrorMessage, out GroupInfo[] GroupList, out int TotalRowCount)
		{
			object[] array = base.Invoke("GroupGetListByGuildMaster", new object[]
			{
				ServerCode,
				OrderType,
				PageNo,
				PageSize,
				NameInGroup_Master_Search
			});
			strErrorMessage = (string)array[1];
			GroupList = (GroupInfo[])array[2];
			TotalRowCount = (int)array[3];
			return (int)array[0];
		}

		public void GroupGetListByGuildMasterAsync(int ServerCode, byte OrderType, int PageNo, byte PageSize, string NameInGroup_Master_Search)
		{
			this.GroupGetListByGuildMasterAsync(ServerCode, OrderType, PageNo, PageSize, NameInGroup_Master_Search, null);
		}

		public void GroupGetListByGuildMasterAsync(int ServerCode, byte OrderType, int PageNo, byte PageSize, string NameInGroup_Master_Search, object userState)
		{
			if (this.GroupGetListByGuildMasterOperationCompleted == null)
			{
				this.GroupGetListByGuildMasterOperationCompleted = new SendOrPostCallback(this.OnGroupGetListByGuildMasterOperationCompleted);
			}
			base.InvokeAsync("GroupGetListByGuildMaster", new object[]
			{
				ServerCode,
				OrderType,
				PageNo,
				PageSize,
				NameInGroup_Master_Search
			}, this.GroupGetListByGuildMasterOperationCompleted, userState);
		}

		private void OnGroupGetListByGuildMasterOperationCompleted(object arg)
		{
			if (this.GroupGetListByGuildMasterCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GroupGetListByGuildMasterCompleted(this, new GroupGetListByGuildMasterCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GroupGetListByGuildName", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GroupGetListByGuildName(int ServerCode, byte OrderType, int PageNo, byte PageSize, string GuildName, out string strErrorMessage, out GroupInfo[] GroupList, out int TotalRowCount)
		{
			object[] array = base.Invoke("GroupGetListByGuildName", new object[]
			{
				ServerCode,
				OrderType,
				PageNo,
				PageSize,
				GuildName
			});
			strErrorMessage = (string)array[1];
			GroupList = (GroupInfo[])array[2];
			TotalRowCount = (int)array[3];
			return (int)array[0];
		}

		public void GroupGetListByGuildNameAsync(int ServerCode, byte OrderType, int PageNo, byte PageSize, string GuildName)
		{
			this.GroupGetListByGuildNameAsync(ServerCode, OrderType, PageNo, PageSize, GuildName, null);
		}

		public void GroupGetListByGuildNameAsync(int ServerCode, byte OrderType, int PageNo, byte PageSize, string GuildName, object userState)
		{
			if (this.GroupGetListByGuildNameOperationCompleted == null)
			{
				this.GroupGetListByGuildNameOperationCompleted = new SendOrPostCallback(this.OnGroupGetListByGuildNameOperationCompleted);
			}
			base.InvokeAsync("GroupGetListByGuildName", new object[]
			{
				ServerCode,
				OrderType,
				PageNo,
				PageSize,
				GuildName
			}, this.GroupGetListByGuildNameOperationCompleted, userState);
		}

		private void OnGroupGetListByGuildNameOperationCompleted(object arg)
		{
			if (this.GroupGetListByGuildNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GroupGetListByGuildNameCompleted(this, new GroupGetListByGuildNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GroupMemberGetInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GroupMemberGetInfo(int ServerCode, int GuildSN, int CharacterSN, string CharacterName, out string strErrorMessage, out GroupMemberInfo MemberInfo)
		{
			object[] array = base.Invoke("GroupMemberGetInfo", new object[]
			{
				ServerCode,
				GuildSN,
				CharacterSN,
				CharacterName
			});
			strErrorMessage = (string)array[1];
			MemberInfo = (GroupMemberInfo)array[2];
			return (int)array[0];
		}

		public void GroupMemberGetInfoAsync(int ServerCode, int GuildSN, int CharacterSN, string CharacterName)
		{
			this.GroupMemberGetInfoAsync(ServerCode, GuildSN, CharacterSN, CharacterName, null);
		}

		public void GroupMemberGetInfoAsync(int ServerCode, int GuildSN, int CharacterSN, string CharacterName, object userState)
		{
			if (this.GroupMemberGetInfoOperationCompleted == null)
			{
				this.GroupMemberGetInfoOperationCompleted = new SendOrPostCallback(this.OnGroupMemberGetInfoOperationCompleted);
			}
			base.InvokeAsync("GroupMemberGetInfo", new object[]
			{
				ServerCode,
				GuildSN,
				CharacterSN,
				CharacterName
			}, this.GroupMemberGetInfoOperationCompleted, userState);
		}

		private void OnGroupMemberGetInfoOperationCompleted(object arg)
		{
			if (this.GroupMemberGetInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GroupMemberGetInfoCompleted(this, new GroupMemberGetInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GroupUserGetInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GroupUserGetInfo(int ServerCode, int NexonSN, out string strErrorMessage, out GroupUserInfo UserInfo)
		{
			object[] array = base.Invoke("GroupUserGetInfo", new object[]
			{
				ServerCode,
				NexonSN
			});
			strErrorMessage = (string)array[1];
			UserInfo = (GroupUserInfo)array[2];
			return (int)array[0];
		}

		public void GroupUserGetInfoAsync(int ServerCode, int NexonSN)
		{
			this.GroupUserGetInfoAsync(ServerCode, NexonSN, null);
		}

		public void GroupUserGetInfoAsync(int ServerCode, int NexonSN, object userState)
		{
			if (this.GroupUserGetInfoOperationCompleted == null)
			{
				this.GroupUserGetInfoOperationCompleted = new SendOrPostCallback(this.OnGroupUserGetInfoOperationCompleted);
			}
			base.InvokeAsync("GroupUserGetInfo", new object[]
			{
				ServerCode,
				NexonSN
			}, this.GroupUserGetInfoOperationCompleted, userState);
		}

		private void OnGroupUserGetInfoOperationCompleted(object arg)
		{
			if (this.GroupUserGetInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GroupUserGetInfoCompleted(this, new GroupUserGetInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GuildClose", RequestElementName = "GuildClose", RequestNamespace = "http://tempuri.org/", ResponseElementName = "GuildCloseResponse", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("GuildCloseResult")]
		public int Remove(int ServerCode, int NexonSN, int GuildSN, out string strErrorMessage)
		{
			object[] array = base.Invoke("Remove", new object[]
			{
				ServerCode,
				NexonSN,
				GuildSN
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void RemoveAsync(int ServerCode, int NexonSN, int GuildSN)
		{
			this.RemoveAsync(ServerCode, NexonSN, GuildSN, null);
		}

		public void RemoveAsync(int ServerCode, int NexonSN, int GuildSN, object userState)
		{
			if (this.RemoveOperationCompleted == null)
			{
				this.RemoveOperationCompleted = new SendOrPostCallback(this.OnRemoveOperationCompleted);
			}
			base.InvokeAsync("Remove", new object[]
			{
				ServerCode,
				NexonSN,
				GuildSN
			}, this.RemoveOperationCompleted, userState);
		}

		private void OnRemoveOperationCompleted(object arg)
		{
			if (this.RemoveCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveCompleted(this, new RemoveCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UserGroupGetList", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int UserGroupGetList(int ServerCode, int NexonSN, int PageNo, byte PageSize, out string strErrorMessage, out UserGroupInfo[] UserGroupList, out int RowCount, out int TotalRowCount)
		{
			object[] array = base.Invoke("UserGroupGetList", new object[]
			{
				ServerCode,
				NexonSN,
				PageNo,
				PageSize
			});
			strErrorMessage = (string)array[1];
			UserGroupList = (UserGroupInfo[])array[2];
			RowCount = (int)array[3];
			TotalRowCount = (int)array[4];
			return (int)array[0];
		}

		public void UserGroupGetListAsync(int ServerCode, int NexonSN, int PageNo, byte PageSize)
		{
			this.UserGroupGetListAsync(ServerCode, NexonSN, PageNo, PageSize, null);
		}

		public void UserGroupGetListAsync(int ServerCode, int NexonSN, int PageNo, byte PageSize, object userState)
		{
			if (this.UserGroupGetListOperationCompleted == null)
			{
				this.UserGroupGetListOperationCompleted = new SendOrPostCallback(this.OnUserGroupGetListOperationCompleted);
			}
			base.InvokeAsync("UserGroupGetList", new object[]
			{
				ServerCode,
				NexonSN,
				PageNo,
				PageSize
			}, this.UserGroupGetListOperationCompleted, userState);
		}

		private void OnUserGroupGetListOperationCompleted(object arg)
		{
			if (this.UserGroupGetListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UserGroupGetListCompleted(this, new UserGroupGetListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UserJoin", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int UserJoin(int ServerCode, int GuildSN, int NexonSN, long CharacterSN, string CharacterName, string Intro, out string strErrorMessage)
		{
			object[] array = base.Invoke("UserJoin", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName,
				Intro
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void UserJoinAsync(int ServerCode, int GuildSN, int NexonSN, long CharacterSN, string CharacterName, string Intro)
		{
			this.UserJoinAsync(ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, Intro, null);
		}

		public void UserJoinAsync(int ServerCode, int GuildSN, int NexonSN, long CharacterSN, string CharacterName, string Intro, object userState)
		{
			if (this.UserJoinOperationCompleted == null)
			{
				this.UserJoinOperationCompleted = new SendOrPostCallback(this.OnUserJoinOperationCompleted);
			}
			base.InvokeAsync("UserJoin", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName,
				Intro
			}, this.UserJoinOperationCompleted, userState);
		}

		private void OnUserJoinOperationCompleted(object arg)
		{
			if (this.UserJoinCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UserJoinCompleted(this, new UserJoinCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UserJoinApply", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int UserJoinApply(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, out string strErrorMessage)
		{
			object[] array = base.Invoke("UserJoinApply", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void UserJoinApplyAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName)
		{
			this.UserJoinApplyAsync(ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, null);
		}

		public void UserJoinApplyAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, object userState)
		{
			if (this.UserJoinApplyOperationCompleted == null)
			{
				this.UserJoinApplyOperationCompleted = new SendOrPostCallback(this.OnUserJoinApplyOperationCompleted);
			}
			base.InvokeAsync("UserJoinApply", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName
			}, this.UserJoinApplyOperationCompleted, userState);
		}

		private void OnUserJoinApplyOperationCompleted(object arg)
		{
			if (this.UserJoinApplyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UserJoinApplyCompleted(this, new UserJoinApplyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UserJoinReject", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int UserJoinReject(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, out string strErrorMessage)
		{
			object[] array = base.Invoke("UserJoinReject", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void UserJoinRejectAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName)
		{
			this.UserJoinRejectAsync(ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, null);
		}

		public void UserJoinRejectAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, object userState)
		{
			if (this.UserJoinRejectOperationCompleted == null)
			{
				this.UserJoinRejectOperationCompleted = new SendOrPostCallback(this.OnUserJoinRejectOperationCompleted);
			}
			base.InvokeAsync("UserJoinReject", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName
			}, this.UserJoinRejectOperationCompleted, userState);
		}

		private void OnUserJoinRejectOperationCompleted(object arg)
		{
			if (this.UserJoinRejectCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UserJoinRejectCompleted(this, new UserJoinRejectCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UserSecede", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int UserSecede(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, out string strErrorMessage)
		{
			object[] array = base.Invoke("UserSecede", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void UserSecedeAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName)
		{
			this.UserSecedeAsync(ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, null);
		}

		public void UserSecedeAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, object userState)
		{
			if (this.UserSecedeOperationCompleted == null)
			{
				this.UserSecedeOperationCompleted = new SendOrPostCallback(this.OnUserSecedeOperationCompleted);
			}
			base.InvokeAsync("UserSecede", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName
			}, this.UserSecedeOperationCompleted, userState);
		}

		private void OnUserSecedeOperationCompleted(object arg)
		{
			if (this.UserSecedeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UserSecedeCompleted(this, new UserSecedeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UserTypeModify", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int UserTypeModify(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, GroupUserType emGroupUserType, out string strErrorMessage)
		{
			object[] array = base.Invoke("UserTypeModify", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName,
				emGroupUserType
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void UserTypeModifyAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, GroupUserType emGroupUserType)
		{
			this.UserTypeModifyAsync(ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, emGroupUserType, null);
		}

		public void UserTypeModifyAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, GroupUserType emGroupUserType, object userState)
		{
			if (this.UserTypeModifyOperationCompleted == null)
			{
				this.UserTypeModifyOperationCompleted = new SendOrPostCallback(this.OnUserTypeModifyOperationCompleted);
			}
			base.InvokeAsync("UserTypeModify", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN,
				CharacterName,
				emGroupUserType
			}, this.UserTypeModifyOperationCompleted, userState);
		}

		private void OnUserTypeModifyOperationCompleted(object arg)
		{
			if (this.UserTypeModifyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UserTypeModifyCompleted(this, new UserTypeModifyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/MemberLogin", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int MemberLogin(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, out string strErrorMessage)
		{
			object[] array = base.Invoke("MemberLogin", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void MemberLoginAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN)
		{
			this.MemberLoginAsync(ServerCode, GuildSN, NexonSN, CharacterSN, null);
		}

		public void MemberLoginAsync(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, object userState)
		{
			if (this.MemberLoginOperationCompleted == null)
			{
				this.MemberLoginOperationCompleted = new SendOrPostCallback(this.OnMemberLoginOperationCompleted);
			}
			base.InvokeAsync("MemberLogin", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN,
				CharacterSN
			}, this.MemberLoginOperationCompleted, userState);
		}

		private void OnMemberLoginOperationCompleted(object arg)
		{
			if (this.MemberLoginCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MemberLoginCompleted(this, new MemberLoginCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GroupUserTryJoin", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GroupUserTryJoin(int ServerCode, long CharacterSN, long NexonSN, string CharacterName, out string strErrorMessage)
		{
			object[] array = base.Invoke("GroupUserTryJoin", new object[]
			{
				ServerCode,
				CharacterSN,
				NexonSN,
				CharacterName
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void GroupUserTryJoinAsync(int ServerCode, long CharacterSN, long NexonSN, string CharacterName)
		{
			this.GroupUserTryJoinAsync(ServerCode, CharacterSN, NexonSN, CharacterName, null);
		}

		public void GroupUserTryJoinAsync(int ServerCode, long CharacterSN, long NexonSN, string CharacterName, object userState)
		{
			if (this.GroupUserTryJoinOperationCompleted == null)
			{
				this.GroupUserTryJoinOperationCompleted = new SendOrPostCallback(this.OnGroupUserTryJoinOperationCompleted);
			}
			base.InvokeAsync("GroupUserTryJoin", new object[]
			{
				ServerCode,
				CharacterSN,
				NexonSN,
				CharacterName
			}, this.GroupUserTryJoinOperationCompleted, userState);
		}

		private void OnGroupUserTryJoinOperationCompleted(object arg)
		{
			if (this.GroupUserTryJoinCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GroupUserTryJoinCompleted(this, new GroupUserTryJoinCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/ChangeMaster", RequestElementName = "ChangeMaster", RequestNamespace = "http://tempuri.org/", ResponseElementName = "ChangeMasterResponse", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ChangeMasterResult")]
		public int GroupChangeMaster(int ServerCode, int GuildSN, long NexonSN_master, long CharacterSN_master, string CharacterName_master, byte codeGroupUserType_oldMaster, out string strErrorMessage)
		{
			object[] array = base.Invoke("GroupChangeMaster", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN_master,
				CharacterSN_master,
				CharacterName_master,
				codeGroupUserType_oldMaster
			});
			strErrorMessage = (string)array[1];
			return (int)array[0];
		}

		public void GroupChangeMasterAsync(int ServerCode, int GuildSN, long NexonSN_master, long CharacterSN_master, string CharacterName_master, byte codeGroupUserType_oldMaster)
		{
			this.GroupChangeMasterAsync(ServerCode, GuildSN, NexonSN_master, CharacterSN_master, CharacterName_master, codeGroupUserType_oldMaster, null);
		}

		public void GroupChangeMasterAsync(int ServerCode, int GuildSN, long NexonSN_master, long CharacterSN_master, string CharacterName_master, byte codeGroupUserType_oldMaster, object userState)
		{
			if (this.GroupChangeMasterOperationCompleted == null)
			{
				this.GroupChangeMasterOperationCompleted = new SendOrPostCallback(this.OnGroupChangeMasterOperationCompleted);
			}
			base.InvokeAsync("GroupChangeMaster", new object[]
			{
				ServerCode,
				GuildSN,
				NexonSN_master,
				CharacterSN_master,
				CharacterName_master,
				codeGroupUserType_oldMaster
			}, this.GroupChangeMasterOperationCompleted, userState);
		}

		private void OnGroupChangeMasterOperationCompleted(object arg)
		{
			if (this.GroupChangeMasterCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GroupChangeMasterCompleted(this, new GroupChangeMasterCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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

		private SendOrPostCallback AllMemberGetListOperationCompleted;

		private SendOrPostCallback CheckGroupIDOperationCompleted;

		private SendOrPostCallback CheckGroupNameOperationCompleted;

		private SendOrPostCallback CreateOperationCompleted;

		private SendOrPostCallback GroupGetInfoByGuildSNOperationCompleted;

		private SendOrPostCallback GroupGetInfoByGuildNameOperationCompleted;

		private SendOrPostCallback GroupGetListByGuildMasterOperationCompleted;

		private SendOrPostCallback GroupGetListByGuildNameOperationCompleted;

		private SendOrPostCallback GroupMemberGetInfoOperationCompleted;

		private SendOrPostCallback GroupUserGetInfoOperationCompleted;

		private SendOrPostCallback RemoveOperationCompleted;

		private SendOrPostCallback UserGroupGetListOperationCompleted;

		private SendOrPostCallback UserJoinOperationCompleted;

		private SendOrPostCallback UserJoinApplyOperationCompleted;

		private SendOrPostCallback UserJoinRejectOperationCompleted;

		private SendOrPostCallback UserSecedeOperationCompleted;

		private SendOrPostCallback UserTypeModifyOperationCompleted;

		private SendOrPostCallback MemberLoginOperationCompleted;

		private SendOrPostCallback GroupUserTryJoinOperationCompleted;

		private SendOrPostCallback GroupChangeMasterOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
