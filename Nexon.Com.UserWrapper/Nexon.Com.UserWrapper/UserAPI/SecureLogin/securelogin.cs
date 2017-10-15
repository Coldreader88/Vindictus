using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Nexon.Com.UserWrapper.Properties;

namespace Nexon.Com.UserWrapper.UserAPI.SecureLogin
{
	[WebServiceBinding(Name = "secureloginSoap", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class securelogin : SoapHttpClientProtocol
	{
		public securelogin()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_UserAPI_SecureLogin_securelogin;
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

		public event GetUserBasicListCompletedEventHandler GetUserBasicListCompleted;

		public event GetOTPRemoveMobileCountCheckCompletedEventHandler GetOTPRemoveMobileCountCheckCompleted;

		public event SendSMSAuthOwnerCfmCompletedEventHandler SendSMSAuthOwnerCfmCompleted;

		public event ConfirmSMSAuthOwnerCfmCompletedEventHandler ConfirmSMSAuthOwnerCfmCompleted;

		public event CheckValidAuthLogSNCompletedEventHandler CheckValidAuthLogSNCompleted;

		public event GetCommonCertAuthCryptCompletedEventHandler GetCommonCertAuthCryptCompleted;

		[SoapDocumentMethod("http://tempuri.org/GetUserBasicInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserBasicInfo(int n4ServiceCode, int n4NexonSN, string strNexonID, string strNexonName, string strPassword, out string strXML)
		{
			object[] array = base.Invoke("GetUserBasicInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				strNexonName,
				strPassword
			});
			strXML = (string)array[1];
			return (int)array[0];
		}

		public void GetUserBasicInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strNexonName, string strPassword)
		{
			this.GetUserBasicInfoAsync(n4ServiceCode, n4NexonSN, strNexonID, strNexonName, strPassword, null);
		}

		public void GetUserBasicInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, string strNexonName, string strPassword, object userState)
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
				strNexonName,
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

		[SoapDocumentMethod("http://tempuri.org/GetUserBasicList", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserBasicList(int n4ServiceCode, string strSsn, bool isTempUserInclude, string strClientIP, out DataSet ndsList, out int n4TotalRowCount, out bool isAdminUser)
		{
			object[] array = base.Invoke("GetUserBasicList", new object[]
			{
				n4ServiceCode,
				strSsn,
				isTempUserInclude,
				strClientIP
			});
			ndsList = (DataSet)array[1];
			n4TotalRowCount = (int)array[2];
			isAdminUser = (bool)array[3];
			return (int)array[0];
		}

		public void GetUserBasicListAsync(int n4ServiceCode, string strSsn, bool isTempUserInclude, string strClientIP)
		{
			this.GetUserBasicListAsync(n4ServiceCode, strSsn, isTempUserInclude, strClientIP, null);
		}

		public void GetUserBasicListAsync(int n4ServiceCode, string strSsn, bool isTempUserInclude, string strClientIP, object userState)
		{
			if (this.GetUserBasicListOperationCompleted == null)
			{
				this.GetUserBasicListOperationCompleted = new SendOrPostCallback(this.OnGetUserBasicListOperationCompleted);
			}
			base.InvokeAsync("GetUserBasicList", new object[]
			{
				n4ServiceCode,
				strSsn,
				isTempUserInclude,
				strClientIP
			}, this.GetUserBasicListOperationCompleted, userState);
		}

		private void OnGetUserBasicListOperationCompleted(object arg)
		{
			if (this.GetUserBasicListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserBasicListCompleted(this, new GetUserBasicListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetOTPRemoveMobileCountCheck", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetOTPRemoveMobileCountCheck(int n4ServiceCode, string strMobilePhone1, string strMobilePhone2, string strMobilePhone3, out bool isEnableOnlineMethod)
		{
			object[] array = base.Invoke("GetOTPRemoveMobileCountCheck", new object[]
			{
				n4ServiceCode,
				strMobilePhone1,
				strMobilePhone2,
				strMobilePhone3
			});
			isEnableOnlineMethod = (bool)array[1];
			return (int)array[0];
		}

		public void GetOTPRemoveMobileCountCheckAsync(int n4ServiceCode, string strMobilePhone1, string strMobilePhone2, string strMobilePhone3)
		{
			this.GetOTPRemoveMobileCountCheckAsync(n4ServiceCode, strMobilePhone1, strMobilePhone2, strMobilePhone3, null);
		}

		public void GetOTPRemoveMobileCountCheckAsync(int n4ServiceCode, string strMobilePhone1, string strMobilePhone2, string strMobilePhone3, object userState)
		{
			if (this.GetOTPRemoveMobileCountCheckOperationCompleted == null)
			{
				this.GetOTPRemoveMobileCountCheckOperationCompleted = new SendOrPostCallback(this.OnGetOTPRemoveMobileCountCheckOperationCompleted);
			}
			base.InvokeAsync("GetOTPRemoveMobileCountCheck", new object[]
			{
				n4ServiceCode,
				strMobilePhone1,
				strMobilePhone2,
				strMobilePhone3
			}, this.GetOTPRemoveMobileCountCheckOperationCompleted, userState);
		}

		private void OnGetOTPRemoveMobileCountCheckOperationCompleted(object arg)
		{
			if (this.GetOTPRemoveMobileCountCheckCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetOTPRemoveMobileCountCheckCompleted(this, new GetOTPRemoveMobileCountCheckCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/SendSMSAuthOwnerCfm", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int SendSMSAuthOwnerCfm(int n4ServiceCode, byte n1AuthUseCode, byte n1MobileCompanyCode, string strMobilePhone1, string strMobilePhone2, string strMobilePhone3, string strSsn, string strName, int n4NexonSN, string strClientIP, out long n8AuthLogSN, out byte n1AuthPGCode, out string strPccSeq, out string strAuthSeq, out string strErrorMessage)
		{
			object[] array = base.Invoke("SendSMSAuthOwnerCfm", new object[]
			{
				n4ServiceCode,
				n1AuthUseCode,
				n1MobileCompanyCode,
				strMobilePhone1,
				strMobilePhone2,
				strMobilePhone3,
				strSsn,
				strName,
				n4NexonSN,
				strClientIP
			});
			n8AuthLogSN = (long)array[1];
			n1AuthPGCode = (byte)array[2];
			strPccSeq = (string)array[3];
			strAuthSeq = (string)array[4];
			strErrorMessage = (string)array[5];
			return (int)array[0];
		}

		public void SendSMSAuthOwnerCfmAsync(int n4ServiceCode, byte n1AuthUseCode, byte n1MobileCompanyCode, string strMobilePhone1, string strMobilePhone2, string strMobilePhone3, string strSsn, string strName, int n4NexonSN, string strClientIP)
		{
			this.SendSMSAuthOwnerCfmAsync(n4ServiceCode, n1AuthUseCode, n1MobileCompanyCode, strMobilePhone1, strMobilePhone2, strMobilePhone3, strSsn, strName, n4NexonSN, strClientIP, null);
		}

		public void SendSMSAuthOwnerCfmAsync(int n4ServiceCode, byte n1AuthUseCode, byte n1MobileCompanyCode, string strMobilePhone1, string strMobilePhone2, string strMobilePhone3, string strSsn, string strName, int n4NexonSN, string strClientIP, object userState)
		{
			if (this.SendSMSAuthOwnerCfmOperationCompleted == null)
			{
				this.SendSMSAuthOwnerCfmOperationCompleted = new SendOrPostCallback(this.OnSendSMSAuthOwnerCfmOperationCompleted);
			}
			base.InvokeAsync("SendSMSAuthOwnerCfm", new object[]
			{
				n4ServiceCode,
				n1AuthUseCode,
				n1MobileCompanyCode,
				strMobilePhone1,
				strMobilePhone2,
				strMobilePhone3,
				strSsn,
				strName,
				n4NexonSN,
				strClientIP
			}, this.SendSMSAuthOwnerCfmOperationCompleted, userState);
		}

		private void OnSendSMSAuthOwnerCfmOperationCompleted(object arg)
		{
			if (this.SendSMSAuthOwnerCfmCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SendSMSAuthOwnerCfmCompleted(this, new SendSMSAuthOwnerCfmCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/ConfirmSMSAuthOwnerCfm", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int ConfirmSMSAuthOwnerCfm(int n4ServiceCode, long n8AuthLogSN, string strInputData)
		{
			object[] array = base.Invoke("ConfirmSMSAuthOwnerCfm", new object[]
			{
				n4ServiceCode,
				n8AuthLogSN,
				strInputData
			});
			return (int)array[0];
		}

		public void ConfirmSMSAuthOwnerCfmAsync(int n4ServiceCode, long n8AuthLogSN, string strInputData)
		{
			this.ConfirmSMSAuthOwnerCfmAsync(n4ServiceCode, n8AuthLogSN, strInputData, null);
		}

		public void ConfirmSMSAuthOwnerCfmAsync(int n4ServiceCode, long n8AuthLogSN, string strInputData, object userState)
		{
			if (this.ConfirmSMSAuthOwnerCfmOperationCompleted == null)
			{
				this.ConfirmSMSAuthOwnerCfmOperationCompleted = new SendOrPostCallback(this.OnConfirmSMSAuthOwnerCfmOperationCompleted);
			}
			base.InvokeAsync("ConfirmSMSAuthOwnerCfm", new object[]
			{
				n4ServiceCode,
				n8AuthLogSN,
				strInputData
			}, this.ConfirmSMSAuthOwnerCfmOperationCompleted, userState);
		}

		private void OnConfirmSMSAuthOwnerCfmOperationCompleted(object arg)
		{
			if (this.ConfirmSMSAuthOwnerCfmCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ConfirmSMSAuthOwnerCfmCompleted(this, new ConfirmSMSAuthOwnerCfmCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckValidAuthLogSN", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckValidAuthLogSN(int n4ServiceCode, long n8AuthLogSN, int n4NexonSN)
		{
			object[] array = base.Invoke("CheckValidAuthLogSN", new object[]
			{
				n4ServiceCode,
				n8AuthLogSN,
				n4NexonSN
			});
			return (int)array[0];
		}

		public void CheckValidAuthLogSNAsync(int n4ServiceCode, long n8AuthLogSN, int n4NexonSN)
		{
			this.CheckValidAuthLogSNAsync(n4ServiceCode, n8AuthLogSN, n4NexonSN, null);
		}

		public void CheckValidAuthLogSNAsync(int n4ServiceCode, long n8AuthLogSN, int n4NexonSN, object userState)
		{
			if (this.CheckValidAuthLogSNOperationCompleted == null)
			{
				this.CheckValidAuthLogSNOperationCompleted = new SendOrPostCallback(this.OnCheckValidAuthLogSNOperationCompleted);
			}
			base.InvokeAsync("CheckValidAuthLogSN", new object[]
			{
				n4ServiceCode,
				n8AuthLogSN,
				n4NexonSN
			}, this.CheckValidAuthLogSNOperationCompleted, userState);
		}

		private void OnCheckValidAuthLogSNOperationCompleted(object arg)
		{
			if (this.CheckValidAuthLogSNCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckValidAuthLogSNCompleted(this, new CheckValidAuthLogSNCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetCommonCertAuthCrypt", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetCommonCertAuthCrypt(int n4ServiceCode, string strName, string strSsn, out string strCertCrypt)
		{
			object[] array = base.Invoke("GetCommonCertAuthCrypt", new object[]
			{
				n4ServiceCode,
				strName,
				strSsn
			});
			strCertCrypt = (string)array[1];
			return (int)array[0];
		}

		public void GetCommonCertAuthCryptAsync(int n4ServiceCode, string strName, string strSsn)
		{
			this.GetCommonCertAuthCryptAsync(n4ServiceCode, strName, strSsn, null);
		}

		public void GetCommonCertAuthCryptAsync(int n4ServiceCode, string strName, string strSsn, object userState)
		{
			if (this.GetCommonCertAuthCryptOperationCompleted == null)
			{
				this.GetCommonCertAuthCryptOperationCompleted = new SendOrPostCallback(this.OnGetCommonCertAuthCryptOperationCompleted);
			}
			base.InvokeAsync("GetCommonCertAuthCrypt", new object[]
			{
				n4ServiceCode,
				strName,
				strSsn
			}, this.GetCommonCertAuthCryptOperationCompleted, userState);
		}

		private void OnGetCommonCertAuthCryptOperationCompleted(object arg)
		{
			if (this.GetCommonCertAuthCryptCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCommonCertAuthCryptCompleted(this, new GetCommonCertAuthCryptCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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

		private SendOrPostCallback GetUserBasicListOperationCompleted;

		private SendOrPostCallback GetOTPRemoveMobileCountCheckOperationCompleted;

		private SendOrPostCallback SendSMSAuthOwnerCfmOperationCompleted;

		private SendOrPostCallback ConfirmSMSAuthOwnerCfmOperationCompleted;

		private SendOrPostCallback CheckValidAuthLogSNOperationCompleted;

		private SendOrPostCallback GetCommonCertAuthCryptOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
