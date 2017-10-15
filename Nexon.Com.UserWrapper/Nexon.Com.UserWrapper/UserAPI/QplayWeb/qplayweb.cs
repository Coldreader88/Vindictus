using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Nexon.Com.UserWrapper.Properties;

namespace Nexon.Com.UserWrapper.UserAPI.QplayWeb
{
	[WebServiceBinding(Name = "qplaywebSoap", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class qplayweb : SoapHttpClientProtocol
	{
		public qplayweb()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_UserAPI_QplayWeb_qplayweb;
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

		public event CheckValidNexonIDCompletedEventHandler CheckValidNexonIDCompleted;

		public event CheckValidNexonIDnPasswordCompletedEventHandler CheckValidNexonIDnPasswordCompleted;

		public event GetUserWriteStatusCodeCompletedEventHandler GetUserWriteStatusCodeCompleted;

		public event GetUserWriteStatusCode2CompletedEventHandler GetUserWriteStatusCode2Completed;

		public event CheckCharacterBlockCompletedEventHandler CheckCharacterBlockCompleted;

		[SoapDocumentMethod("http://tempuri.org/CheckValidNexonID", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckValidNexonID(int n4ServiceCode, string strNexonID, out string strSsn)
		{
			object[] array = base.Invoke("CheckValidNexonID", new object[]
			{
				n4ServiceCode,
				strNexonID
			});
			strSsn = (string)array[1];
			return (int)array[0];
		}

		public void CheckValidNexonIDAsync(int n4ServiceCode, string strNexonID)
		{
			this.CheckValidNexonIDAsync(n4ServiceCode, strNexonID, null);
		}

		public void CheckValidNexonIDAsync(int n4ServiceCode, string strNexonID, object userState)
		{
			if (this.CheckValidNexonIDOperationCompleted == null)
			{
				this.CheckValidNexonIDOperationCompleted = new SendOrPostCallback(this.OnCheckValidNexonIDOperationCompleted);
			}
			base.InvokeAsync("CheckValidNexonID", new object[]
			{
				n4ServiceCode,
				strNexonID
			}, this.CheckValidNexonIDOperationCompleted, userState);
		}

		private void OnCheckValidNexonIDOperationCompleted(object arg)
		{
			if (this.CheckValidNexonIDCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckValidNexonIDCompleted(this, new CheckValidNexonIDCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckValidNexonIDnPassword", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckValidNexonIDnPassword(int n4ServiceCode, string strNexonID, string strPassword, out string strSsn)
		{
			object[] array = base.Invoke("CheckValidNexonIDnPassword", new object[]
			{
				n4ServiceCode,
				strNexonID,
				strPassword
			});
			strSsn = (string)array[1];
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

		[SoapDocumentMethod("http://tempuri.org/GetUserWriteStatusCode", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserWriteStatusCode(int n4ServiceCode, string strNexonID, int n4NexonSN, out byte n1WriteStatusCode)
		{
			object[] array = base.Invoke("GetUserWriteStatusCode", new object[]
			{
				n4ServiceCode,
				strNexonID,
				n4NexonSN
			});
			n1WriteStatusCode = (byte)array[1];
			return (int)array[0];
		}

		public void GetUserWriteStatusCodeAsync(int n4ServiceCode, string strNexonID, int n4NexonSN)
		{
			this.GetUserWriteStatusCodeAsync(n4ServiceCode, strNexonID, n4NexonSN, null);
		}

		public void GetUserWriteStatusCodeAsync(int n4ServiceCode, string strNexonID, int n4NexonSN, object userState)
		{
			if (this.GetUserWriteStatusCodeOperationCompleted == null)
			{
				this.GetUserWriteStatusCodeOperationCompleted = new SendOrPostCallback(this.OnGetUserWriteStatusCodeOperationCompleted);
			}
			base.InvokeAsync("GetUserWriteStatusCode", new object[]
			{
				n4ServiceCode,
				strNexonID,
				n4NexonSN
			}, this.GetUserWriteStatusCodeOperationCompleted, userState);
		}

		private void OnGetUserWriteStatusCodeOperationCompleted(object arg)
		{
			if (this.GetUserWriteStatusCodeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserWriteStatusCodeCompleted(this, new GetUserWriteStatusCodeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetUserWriteStatusCode2", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserWriteStatusCode2(int n4ServiceCode, string strNexonID, out byte n1WriteStatusCode, out int n4NexonSN)
		{
			object[] array = base.Invoke("GetUserWriteStatusCode2", new object[]
			{
				n4ServiceCode,
				strNexonID
			});
			n1WriteStatusCode = (byte)array[1];
			n4NexonSN = (int)array[2];
			return (int)array[0];
		}

		public void GetUserWriteStatusCode2Async(int n4ServiceCode, string strNexonID)
		{
			this.GetUserWriteStatusCode2Async(n4ServiceCode, strNexonID, null);
		}

		public void GetUserWriteStatusCode2Async(int n4ServiceCode, string strNexonID, object userState)
		{
			if (this.GetUserWriteStatusCode2OperationCompleted == null)
			{
				this.GetUserWriteStatusCode2OperationCompleted = new SendOrPostCallback(this.OnGetUserWriteStatusCode2OperationCompleted);
			}
			base.InvokeAsync("GetUserWriteStatusCode2", new object[]
			{
				n4ServiceCode,
				strNexonID
			}, this.GetUserWriteStatusCode2OperationCompleted, userState);
		}

		private void OnGetUserWriteStatusCode2OperationCompleted(object arg)
		{
			if (this.GetUserWriteStatusCode2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserWriteStatusCode2Completed(this, new GetUserWriteStatusCode2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckCharacterBlock", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckCharacterBlock(int n4ServiceCode, string strNexonID)
		{
			object[] array = base.Invoke("CheckCharacterBlock", new object[]
			{
				n4ServiceCode,
				strNexonID
			});
			return (int)array[0];
		}

		public void CheckCharacterBlockAsync(int n4ServiceCode, string strNexonID)
		{
			this.CheckCharacterBlockAsync(n4ServiceCode, strNexonID, null);
		}

		public void CheckCharacterBlockAsync(int n4ServiceCode, string strNexonID, object userState)
		{
			if (this.CheckCharacterBlockOperationCompleted == null)
			{
				this.CheckCharacterBlockOperationCompleted = new SendOrPostCallback(this.OnCheckCharacterBlockOperationCompleted);
			}
			base.InvokeAsync("CheckCharacterBlock", new object[]
			{
				n4ServiceCode,
				strNexonID
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

		private SendOrPostCallback CheckValidNexonIDOperationCompleted;

		private SendOrPostCallback CheckValidNexonIDnPasswordOperationCompleted;

		private SendOrPostCallback GetUserWriteStatusCodeOperationCompleted;

		private SendOrPostCallback GetUserWriteStatusCode2OperationCompleted;

		private SendOrPostCallback CheckCharacterBlockOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
