using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Nexon.Com.UserWrapper.Properties;

namespace Nexon.Com.UserWrapper.UserAPI.GuildInGame
{
	[WebServiceBinding(Name = "guildingameSoap", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class guildingame : SoapHttpClientProtocol
	{
		public guildingame()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_Nexon_Com_UserWrapper_UserAPI_GuildInGame_guildingame;
			if (this.IsLocalFileSystemWebService(this.Url))
			{
				this.UseDefaultCredentials = true;
				this.useDefaultCredentialsSetExplicitly = false;
				return;
			}
			this.useDefaultCredentialsSetExplicitly = true;
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

		public event GetUserInfonSchoolInfoCompletedEventHandler GetUserInfonSchoolInfoCompleted;

		[SoapDocumentMethod("http://tempuri.org/GetUserInfonSchoolInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserInfonSchoolInfo(int n4ServiceCode, int n4NexonSN, string strNexonID, string strPassword, out string strBasicInfoXML, out int n4SchoolSN, out string strSchoolName)
		{
			object[] array = base.Invoke("GetUserInfonSchoolInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				strPassword
			});
			strBasicInfoXML = (string)array[1];
			n4SchoolSN = (int)array[2];
			strSchoolName = (string)array[3];
			return (int)array[0];
		}

		public void GetUserInfonSchoolInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strPassword)
		{
			this.GetUserInfonSchoolInfoAsync(n4ServiceCode, n4NexonSN, strNexonID, strPassword, null);
		}

		public void GetUserInfonSchoolInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strPassword, object userState)
		{
			if (this.GetUserInfonSchoolInfoOperationCompleted == null)
			{
				this.GetUserInfonSchoolInfoOperationCompleted = new SendOrPostCallback(this.OnGetUserInfonSchoolInfoOperationCompleted);
			}
			base.InvokeAsync("GetUserInfonSchoolInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				strPassword
			}, this.GetUserInfonSchoolInfoOperationCompleted, userState);
		}

		private void OnGetUserInfonSchoolInfoOperationCompleted(object arg)
		{
			if (this.GetUserInfonSchoolInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserInfonSchoolInfoCompleted(this, new GetUserInfonSchoolInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			if (url == null || url == string.Empty)
			{
				return false;
			}
			Uri uri = new Uri(url);
			return uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
		}

		private SendOrPostCallback GetUserInfonSchoolInfoOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
