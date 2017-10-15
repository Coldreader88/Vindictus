using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Nexon.Com.UserWrapper.Properties;

namespace Nexon.Com.UserWrapper.UserAPI.SEOUL2012Web
{
	[WebServiceBinding(Name = "seoul2012webSoap", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DesignerCategory("code")]
	public class seoul2012web : SoapHttpClientProtocol
	{
		public seoul2012web()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_Nexon_Com_UserWrapper_UserAPI_SEOUL2012Web_seoul2012web;
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

		public event GetUserIdentitySNCompletedEventHandler GetUserIdentitySNCompleted;

		[SoapDocumentMethod("http://tempuri.org/GetUserIdentitySN", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserIdentitySN(int n4ServiceCode, string strNexonID, out long n8IdentitySN)
		{
			object[] array = base.Invoke("GetUserIdentitySN", new object[]
			{
				n4ServiceCode,
				strNexonID
			});
			n8IdentitySN = (long)array[1];
			return (int)array[0];
		}

		public void GetUserIdentitySNAsync(int n4ServiceCode, string strNexonID)
		{
			this.GetUserIdentitySNAsync(n4ServiceCode, strNexonID, null);
		}

		public void GetUserIdentitySNAsync(int n4ServiceCode, string strNexonID, object userState)
		{
			if (this.GetUserIdentitySNOperationCompleted == null)
			{
				this.GetUserIdentitySNOperationCompleted = new SendOrPostCallback(this.OnGetUserIdentitySNOperationCompleted);
			}
			base.InvokeAsync("GetUserIdentitySN", new object[]
			{
				n4ServiceCode,
				strNexonID
			}, this.GetUserIdentitySNOperationCompleted, userState);
		}

		private void OnGetUserIdentitySNOperationCompleted(object arg)
		{
			if (this.GetUserIdentitySNCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserIdentitySNCompleted(this, new GetUserIdentitySNCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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

		private SendOrPostCallback GetUserIdentitySNOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
