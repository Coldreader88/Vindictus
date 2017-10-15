using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Nexon.Com.UserWrapper.Properties;

namespace Nexon.Com.UserWrapper.UserAPI.FreeCash
{
	[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
	[DebuggerStepThrough]
	[WebServiceBinding(Name = "freecashSoap", Namespace = "http://tempuri.org/")]
	[DesignerCategory("code")]
	public class freecash : SoapHttpClientProtocol
	{
		public freecash()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_UserAPI_FreeCash_freecash;
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

		public event GetUserBasicInfoCompletedEventHandler GetUserBasicInfoCompleted;

		[SoapDocumentMethod("http://tempuri.org/GetUserBasicInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserBasicInfo(int n4ServiceCode, int n4NexonSN, string strNexonID, out bool isHasSsn, out byte n1Age, out bool isTempUser_child)
		{
			object[] array = base.Invoke("GetUserBasicInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID
			});
			isHasSsn = (bool)array[1];
			n1Age = (byte)array[2];
			isTempUser_child = (bool)array[3];
			return (int)array[0];
		}

		public void GetUserBasicInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID)
		{
			this.GetUserBasicInfoAsync(n4ServiceCode, n4NexonSN, strNexonID, null);
		}

		public void GetUserBasicInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, object userState)
		{
			if (this.GetUserBasicInfoOperationCompleted == null)
			{
				this.GetUserBasicInfoOperationCompleted = new SendOrPostCallback(this.OnGetUserBasicInfoOperationCompleted);
			}
			base.InvokeAsync("GetUserBasicInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID
			}, this.GetUserBasicInfoOperationCompleted, userState);
		}

		private void OnGetUserBasicInfoOperationCompleted(object arg)
		{
			if (this.GetUserBasicInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserBasicInfoCompleted(this, new GetUserBasicInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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

		private SendOrPostCallback GetUserBasicInfoOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
