using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Nexon.Com.UserWrapper.Properties;

namespace Nexon.Com.UserWrapper.UserAPI.NexonStick
{
	[WebServiceBinding(Name = "nexonstickSoap", Namespace = "http://api.user.nexon.com/soap/nexonstick/")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	public class nexonstick : SoapHttpClientProtocol
	{
		public nexonstick()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_Nexon_Com_UserWrapper_UserAPI_NexonStick_nexonstick;
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

		public event CheckMatchUserSsn_FullCompletedEventHandler CheckMatchUserSsn_FullCompleted;

		public event CheckValidNexonIDnPasswordCompletedEventHandler CheckValidNexonIDnPasswordCompleted;

		public event GetUserBasicInfoCompletedEventHandler GetUserBasicInfoCompleted;

		public event CheckEnableMobileAuthCompletedEventHandler CheckEnableMobileAuthCompleted;

		public event SendSMSAuthOwnerCfmCompletedEventHandler SendSMSAuthOwnerCfmCompleted;

		public event ConfirmSMSAuthOwnerCfmCompletedEventHandler ConfirmSMSAuthOwnerCfmCompleted;

		public event CheckValidAuthLogSNCompletedEventHandler CheckValidAuthLogSNCompleted;

		public event GetCommonCertAuthCryptCompletedEventHandler GetCommonCertAuthCryptCompleted;

		[SoapDocumentMethod("http://api.user.nexon.com/soap/nexonstick/CheckMatchUserSsn_Full", RequestNamespace = "http://api.user.nexon.com/soap/nexonstick/", ResponseNamespace = "http://api.user.nexon.com/soap/nexonstick/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckMatchUserSsn_Full(int n4ServiceCode, string strNexonID, string strSsn)
		{
			object[] array = base.Invoke("CheckMatchUserSsn_Full", new object[]
			{
				n4ServiceCode,
				strNexonID,
				strSsn
			});
			return (int)array[0];
		}

		public void CheckMatchUserSsn_FullAsync(int n4ServiceCode, string strNexonID, string strSsn)
		{
			this.CheckMatchUserSsn_FullAsync(n4ServiceCode, strNexonID, strSsn, null);
		}

		public void CheckMatchUserSsn_FullAsync(int n4ServiceCode, string strNexonID, string strSsn, object userState)
		{
			if (this.CheckMatchUserSsn_FullOperationCompleted == null)
			{
				this.CheckMatchUserSsn_FullOperationCompleted = new SendOrPostCallback(this.OnCheckMatchUserSsn_FullOperationCompleted);
			}
			base.InvokeAsync("CheckMatchUserSsn_Full", new object[]
			{
				n4ServiceCode,
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

		[SoapDocumentMethod("http://api.user.nexon.com/soap/nexonstick/CheckValidNexonIDnPassword", RequestNamespace = "http://api.user.nexon.com/soap/nexonstick/", ResponseNamespace = "http://api.user.nexon.com/soap/nexonstick/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckValidNexonIDnPassword(int n4ServiceCode, string strNexonID, string strPassword, out int n4NexonSN)
		{
			object[] array = base.Invoke("CheckValidNexonIDnPassword", new object[]
			{
				n4ServiceCode,
				strNexonID,
				strPassword
			});
			n4NexonSN = (int)array[1];
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

		[SoapDocumentMethod("http://api.user.nexon.com/soap/nexonstick/GetUserBasicInfo", RequestNamespace = "http://api.user.nexon.com/soap/nexonstick/", ResponseNamespace = "http://api.user.nexon.com/soap/nexonstick/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
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

		[SoapDocumentMethod("http://api.user.nexon.com/soap/nexonstick/CheckEnableMobileAuth", RequestNamespace = "http://api.user.nexon.com/soap/nexonstick/", ResponseNamespace = "http://api.user.nexon.com/soap/nexonstick/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckEnableMobileAuth(int n4ServiceCode, byte n1AuthUseCode, string strSsn, out bool isEnableOnlineMethod)
		{
			object[] array = base.Invoke("CheckEnableMobileAuth", new object[]
			{
				n4ServiceCode,
				n1AuthUseCode,
				strSsn
			});
			isEnableOnlineMethod = (bool)array[1];
			return (int)array[0];
		}

		public void CheckEnableMobileAuthAsync(int n4ServiceCode, byte n1AuthUseCode, string strSsn)
		{
			this.CheckEnableMobileAuthAsync(n4ServiceCode, n1AuthUseCode, strSsn, null);
		}

		public void CheckEnableMobileAuthAsync(int n4ServiceCode, byte n1AuthUseCode, string strSsn, object userState)
		{
			if (this.CheckEnableMobileAuthOperationCompleted == null)
			{
				this.CheckEnableMobileAuthOperationCompleted = new SendOrPostCallback(this.OnCheckEnableMobileAuthOperationCompleted);
			}
			base.InvokeAsync("CheckEnableMobileAuth", new object[]
			{
				n4ServiceCode,
				n1AuthUseCode,
				strSsn
			}, this.CheckEnableMobileAuthOperationCompleted, userState);
		}

		private void OnCheckEnableMobileAuthOperationCompleted(object arg)
		{
			if (this.CheckEnableMobileAuthCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckEnableMobileAuthCompleted(this, new CheckEnableMobileAuthCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://api.user.nexon.com/soap/nexonstick/SendSMSAuthOwnerCfm", RequestNamespace = "http://api.user.nexon.com/soap/nexonstick/", ResponseNamespace = "http://api.user.nexon.com/soap/nexonstick/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
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

		[SoapDocumentMethod("http://api.user.nexon.com/soap/nexonstick/ConfirmSMSAuthOwnerCfm", RequestNamespace = "http://api.user.nexon.com/soap/nexonstick/", ResponseNamespace = "http://api.user.nexon.com/soap/nexonstick/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
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

		[SoapDocumentMethod("http://api.user.nexon.com/soap/nexonstick/CheckValidAuthLogSN", RequestNamespace = "http://api.user.nexon.com/soap/nexonstick/", ResponseNamespace = "http://api.user.nexon.com/soap/nexonstick/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
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

		[SoapDocumentMethod("http://api.user.nexon.com/soap/nexonstick/GetCommonCertAuthCrypt", RequestNamespace = "http://api.user.nexon.com/soap/nexonstick/", ResponseNamespace = "http://api.user.nexon.com/soap/nexonstick/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
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

		private SendOrPostCallback CheckMatchUserSsn_FullOperationCompleted;

		private SendOrPostCallback CheckValidNexonIDnPasswordOperationCompleted;

		private SendOrPostCallback GetUserBasicInfoOperationCompleted;

		private SendOrPostCallback CheckEnableMobileAuthOperationCompleted;

		private SendOrPostCallback SendSMSAuthOwnerCfmOperationCompleted;

		private SendOrPostCallback ConfirmSMSAuthOwnerCfmOperationCompleted;

		private SendOrPostCallback CheckValidAuthLogSNOperationCompleted;

		private SendOrPostCallback GetCommonCertAuthCryptOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
