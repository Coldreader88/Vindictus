using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Nexon.Com.UserWrapper.Properties;

namespace Nexon.Com.UserWrapper.UserAPI.GuestUser
{
	[DebuggerStepThrough]
	[WebServiceBinding(Name = "guestuserSoap", Namespace = "http://tempuri.org/")]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	public class guestuser : SoapHttpClientProtocol
	{
		public guestuser()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_UserAPI_GuestUser_guestuser;
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

		public event CheckEnableChangeUserCompletedEventHandler CheckEnableChangeUserCompleted;

		[SoapDocumentMethod("http://tempuri.org/CheckEnableChangeUser", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckEnableChangeUser(int n4ServiceCode, string strNexonID, string strPassword, string strSsn_Sub, out int n4NexonSN)
		{
			object[] array = base.Invoke("CheckEnableChangeUser", new object[]
			{
				n4ServiceCode,
				strNexonID,
				strPassword,
				strSsn_Sub
			});
			n4NexonSN = (int)array[1];
			return (int)array[0];
		}

		public void CheckEnableChangeUserAsync(int n4ServiceCode, string strNexonID, string strPassword, string strSsn_Sub)
		{
			this.CheckEnableChangeUserAsync(n4ServiceCode, strNexonID, strPassword, strSsn_Sub, null);
		}

		public void CheckEnableChangeUserAsync(int n4ServiceCode, string strNexonID, string strPassword, string strSsn_Sub, object userState)
		{
			if (this.CheckEnableChangeUserOperationCompleted == null)
			{
				this.CheckEnableChangeUserOperationCompleted = new SendOrPostCallback(this.OnCheckEnableChangeUserOperationCompleted);
			}
			base.InvokeAsync("CheckEnableChangeUser", new object[]
			{
				n4ServiceCode,
				strNexonID,
				strPassword,
				strSsn_Sub
			}, this.CheckEnableChangeUserOperationCompleted, userState);
		}

		private void OnCheckEnableChangeUserOperationCompleted(object arg)
		{
			if (this.CheckEnableChangeUserCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckEnableChangeUserCompleted(this, new CheckEnableChangeUserCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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

		private SendOrPostCallback CheckEnableChangeUserOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
