using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Nexon.Com.UserWrapper.Properties;

namespace Nexon.Com.UserWrapper.UserAPI.Character
{
	[WebServiceBinding(Name = "characterSoap", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	public class character : SoapHttpClientProtocol
	{
		public character()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_UserAPI_Character_character;
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

		public event CheckCharacterBlockCompletedEventHandler CheckCharacterBlockCompleted;

		public event CheckMatchUserSsn_SubCompletedEventHandler CheckMatchUserSsn_SubCompleted;

		public event CheckMatchUserSsn_FullCompletedEventHandler CheckMatchUserSsn_FullCompleted;

		public event CheckValidNexonIDnPasswordCompletedEventHandler CheckValidNexonIDnPasswordCompleted;

		public event GetUserBasicInfoCompletedEventHandler GetUserBasicInfoCompleted;

		[SoapDocumentMethod("http://tempuri.org/CheckCharacterBlock", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckCharacterBlock(int n4ServiceCode, int n4NexonSN)
		{
			object[] array = base.Invoke("CheckCharacterBlock", new object[]
			{
				n4ServiceCode,
				n4NexonSN
			});
			return (int)array[0];
		}

		public void CheckCharacterBlockAsync(int n4ServiceCode, int n4NexonSN)
		{
			this.CheckCharacterBlockAsync(n4ServiceCode, n4NexonSN, null);
		}

		public void CheckCharacterBlockAsync(int n4ServiceCode, int n4NexonSN, object userState)
		{
			if (this.CheckCharacterBlockOperationCompleted == null)
			{
				this.CheckCharacterBlockOperationCompleted = new SendOrPostCallback(this.OnCheckCharacterBlockOperationCompleted);
			}
			base.InvokeAsync("CheckCharacterBlock", new object[]
			{
				n4ServiceCode,
				n4NexonSN
			}, this.CheckCharacterBlockOperationCompleted, userState);
		}

		private void OnCheckCharacterBlockOperationCompleted(object arg)
		{
			if (this.CheckCharacterBlockCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckCharacterBlockCompleted(this, new CheckCharacterBlockCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckMatchUserSsn_Sub", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckMatchUserSsn_Sub(int n4ServiceCode, int n4NexonSN, string strNexonID, string strSsn_Sub)
		{
			object[] array = base.Invoke("CheckMatchUserSsn_Sub", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				strSsn_Sub
			});
			return (int)array[0];
		}

		public void CheckMatchUserSsn_SubAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strSsn_Sub)
		{
			this.CheckMatchUserSsn_SubAsync(n4ServiceCode, n4NexonSN, strNexonID, strSsn_Sub, null);
		}

		public void CheckMatchUserSsn_SubAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strSsn_Sub, object userState)
		{
			if (this.CheckMatchUserSsn_SubOperationCompleted == null)
			{
				this.CheckMatchUserSsn_SubOperationCompleted = new SendOrPostCallback(this.OnCheckMatchUserSsn_SubOperationCompleted);
			}
			base.InvokeAsync("CheckMatchUserSsn_Sub", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				strSsn_Sub
			}, this.CheckMatchUserSsn_SubOperationCompleted, userState);
		}

		private void OnCheckMatchUserSsn_SubOperationCompleted(object arg)
		{
			if (this.CheckMatchUserSsn_SubCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckMatchUserSsn_SubCompleted(this, new CheckMatchUserSsn_SubCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckMatchUserSsn_Full", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckMatchUserSsn_Full(int n4ServiceCode, int n4NexonSN, string strNexonID, string strSsn)
		{
			object[] array = base.Invoke("CheckMatchUserSsn_Full", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				strSsn
			});
			return (int)array[0];
		}

		public void CheckMatchUserSsn_FullAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strSsn)
		{
			this.CheckMatchUserSsn_FullAsync(n4ServiceCode, n4NexonSN, strNexonID, strSsn, null);
		}

		public void CheckMatchUserSsn_FullAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strSsn, object userState)
		{
			if (this.CheckMatchUserSsn_FullOperationCompleted == null)
			{
				this.CheckMatchUserSsn_FullOperationCompleted = new SendOrPostCallback(this.OnCheckMatchUserSsn_FullOperationCompleted);
			}
			base.InvokeAsync("CheckMatchUserSsn_Full", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				strSsn
			}, this.CheckMatchUserSsn_FullOperationCompleted, userState);
		}

		private void OnCheckMatchUserSsn_FullOperationCompleted(object arg)
		{
			if (this.CheckMatchUserSsn_FullCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckMatchUserSsn_FullCompleted(this, new CheckMatchUserSsn_FullCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckValidNexonIDnPassword", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckValidNexonIDnPassword(int n4ServiceCode, string strNexonID, string strPassword)
		{
			object[] array = base.Invoke("CheckValidNexonIDnPassword", new object[]
			{
				n4ServiceCode,
				strNexonID,
				strPassword
			});
			return (int)array[0];
		}

		public void CheckValidNexonIDnPasswordAsync(int n4ServiceCode, string strNexonID, string strPassword)
		{
			this.CheckValidNexonIDnPasswordAsync(n4ServiceCode, strNexonID, strPassword, null);
		}

		public void CheckValidNexonIDnPasswordAsync(int n4ServiceCode, string strNexonID, string strPassword, object userState)
		{
			if (this.CheckValidNexonIDnPasswordOperationCompleted == null)
			{
				this.CheckValidNexonIDnPasswordOperationCompleted = new SendOrPostCallback(this.OnCheckValidNexonIDnPasswordOperationCompleted);
			}
			base.InvokeAsync("CheckValidNexonIDnPassword", new object[]
			{
				n4ServiceCode,
				strNexonID,
				strPassword
			}, this.CheckValidNexonIDnPasswordOperationCompleted, userState);
		}

		private void OnCheckValidNexonIDnPasswordOperationCompleted(object arg)
		{
			if (this.CheckValidNexonIDnPasswordCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckValidNexonIDnPasswordCompleted(this, new CheckValidNexonIDnPasswordCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetUserBasicInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserBasicInfo(int n4ServiceCode, int n4NexonSN, string strNexonID, string strPassword, out string strXML)
		{
			object[] array = base.Invoke("GetUserBasicInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				strPassword
			});
			strXML = (string)array[1];
			return (int)array[0];
		}

		public void GetUserBasicInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strPassword)
		{
			this.GetUserBasicInfoAsync(n4ServiceCode, n4NexonSN, strNexonID, strPassword, null);
		}

		public void GetUserBasicInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strPassword, object userState)
		{
			if (this.GetUserBasicInfoOperationCompleted == null)
			{
				this.GetUserBasicInfoOperationCompleted = new SendOrPostCallback(this.OnGetUserBasicInfoOperationCompleted);
			}
			base.InvokeAsync("GetUserBasicInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				strPassword
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

		private SendOrPostCallback CheckCharacterBlockOperationCompleted;

		private SendOrPostCallback CheckMatchUserSsn_SubOperationCompleted;

		private SendOrPostCallback CheckMatchUserSsn_FullOperationCompleted;

		private SendOrPostCallback CheckValidNexonIDnPasswordOperationCompleted;

		private SendOrPostCallback GetUserBasicInfoOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
