using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using NPL.SSO.Properties;

namespace NPL.SSO.com.nexon.auth
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "DefaultSoap", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class Default : SoapHttpClientProtocol
	{
		public Default()
		{
			this.Url = Settings.Default.NPL_SSO_com_nexon_auth_Default;
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

		public event LoginCompletedEventHandler LoginCompleted;

		public event Login2CompletedEventHandler Login2Completed;

		public event Login3CompletedEventHandler Login3Completed;

		public event Login4CompletedEventHandler Login4Completed;

		public event AuthorizedLogin64CompletedEventHandler AuthorizedLogin64Completed;

		public event ChannelingLoginCompletedEventHandler ChannelingLoginCompleted;

		public event MobileLoginCompletedEventHandler MobileLoginCompleted;

		public event PCBangLoginCompletedEventHandler PCBangLoginCompleted;

		public event LogoutCompletedEventHandler LogoutCompleted;

		public event AttachSessionCompletedEventHandler AttachSessionCompleted;

		public event UpdateSessionCompletedEventHandler UpdateSessionCompleted;

		public event CheckSessionCompletedEventHandler CheckSessionCompleted;

		public event CheckSession2CompletedEventHandler CheckSession2Completed;

		public event CheckAndAttachSessionCompletedEventHandler CheckAndAttachSessionCompleted;

		public event CheckSessionAndUpdateInfoCompletedEventHandler CheckSessionAndUpdateInfoCompleted;

		public event CheckSessionAndGetInfoCompletedEventHandler CheckSessionAndGetInfoCompleted;

		public event CheckSessionAndGetInfo2CompletedEventHandler CheckSessionAndGetInfo2Completed;

		public event CheckSessionAndGetInfo3CompletedEventHandler CheckSessionAndGetInfo3Completed;

		public event CheckSessionAndGetInfo4CompletedEventHandler CheckSessionAndGetInfo4Completed;

		public event CheckIDnPasswordCompletedEventHandler CheckIDnPasswordCompleted;

		public event UpdateInfoCompletedEventHandler UpdateInfoCompleted;

		public event UpdateInfo64CompletedEventHandler UpdateInfo64Completed;

		public event GetOldFashionInfoCompletedEventHandler GetOldFashionInfoCompleted;

		public event UpgradeGameTokenCompletedEventHandler UpgradeGameTokenCompleted;

		public event BanCompletedEventHandler BanCompleted;

		public event IsOTPUsableCompletedEventHandler IsOTPUsableCompleted;

		public event IsOTPUsable64CompletedEventHandler IsOTPUsable64Completed;

		public event IsLoginCompletedEventHandler IsLoginCompleted;

		public event DualTestLoginCompletedEventHandler DualTestLoginCompleted;

		public event DualTestLogoutCompletedEventHandler DualTestLogoutCompleted;

		public event DualTestCheckSessionCompletedEventHandler DualTestCheckSessionCompleted;

		[SoapDocumentMethod("http://tempuri.org/Login", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public LoginResult Login(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale)
		{
			object[] array = base.Invoke("Login", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale
			});
			return (LoginResult)array[0];
		}

		public void LoginAsync(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale)
		{
			this.LoginAsync(strNexonID, strPwd, strIP, uGameCode, uLocale, null);
		}

		public void LoginAsync(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale, object userState)
		{
			if (this.LoginOperationCompleted == null)
			{
				this.LoginOperationCompleted = new SendOrPostCallback(this.OnLoginOperationCompleted);
			}
			base.InvokeAsync("Login", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale
			}, this.LoginOperationCompleted, userState);
		}

		private void OnLoginOperationCompleted(object arg)
		{
			if (this.LoginCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoginCompleted(this, new LoginCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/Login2", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public LoginResult Login2(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale)
		{
			object[] array = base.Invoke("Login2", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale
			});
			return (LoginResult)array[0];
		}

		public void Login2Async(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale)
		{
			this.Login2Async(strNexonID, strPwd, strIP, uGameCode, uLocale, null);
		}

		public void Login2Async(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale, object userState)
		{
			if (this.Login2OperationCompleted == null)
			{
				this.Login2OperationCompleted = new SendOrPostCallback(this.OnLogin2OperationCompleted);
			}
			base.InvokeAsync("Login2", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale
			}, this.Login2OperationCompleted, userState);
		}

		private void OnLogin2OperationCompleted(object arg)
		{
			if (this.Login2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.Login2Completed(this, new Login2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/Login3", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public LoginResult Login3(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale, uint uRegionCode)
		{
			object[] array = base.Invoke("Login3", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale,
				uRegionCode
			});
			return (LoginResult)array[0];
		}

		public void Login3Async(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale, uint uRegionCode)
		{
			this.Login3Async(strNexonID, strPwd, strIP, uGameCode, uLocale, uRegionCode, null);
		}

		public void Login3Async(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale, uint uRegionCode, object userState)
		{
			if (this.Login3OperationCompleted == null)
			{
				this.Login3OperationCompleted = new SendOrPostCallback(this.OnLogin3OperationCompleted);
			}
			base.InvokeAsync("Login3", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale,
				uRegionCode
			}, this.Login3OperationCompleted, userState);
		}

		private void OnLogin3OperationCompleted(object arg)
		{
			if (this.Login3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.Login3Completed(this, new Login3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/Login4", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public LoginResult2 Login4(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale, uint uRegionCode)
		{
			object[] array = base.Invoke("Login4", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale,
				uRegionCode
			});
			return (LoginResult2)array[0];
		}

		public void Login4Async(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale, uint uRegionCode)
		{
			this.Login4Async(strNexonID, strPwd, strIP, uGameCode, uLocale, uRegionCode, null);
		}

		public void Login4Async(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale, uint uRegionCode, object userState)
		{
			if (this.Login4OperationCompleted == null)
			{
				this.Login4OperationCompleted = new SendOrPostCallback(this.OnLogin4OperationCompleted);
			}
			base.InvokeAsync("Login4", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale,
				uRegionCode
			}, this.Login4OperationCompleted, userState);
		}

		private void OnLogin4OperationCompleted(object arg)
		{
			if (this.Login4Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.Login4Completed(this, new Login4CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/AuthorizedLogin64", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public AuthorizedLoginResult64 AuthorizedLogin64(byte uSecureCode, string strNexonID, long nNexonSN64, string strIP, string strHWID, uint uGameCode, uint uLocale)
		{
			object[] array = base.Invoke("AuthorizedLogin64", new object[]
			{
				uSecureCode,
				strNexonID,
				nNexonSN64,
				strIP,
				strHWID,
				uGameCode,
				uLocale
			});
			return (AuthorizedLoginResult64)array[0];
		}

		public void AuthorizedLogin64Async(byte uSecureCode, string strNexonID, long nNexonSN64, string strIP, string strHWID, uint uGameCode, uint uLocale)
		{
			this.AuthorizedLogin64Async(uSecureCode, strNexonID, nNexonSN64, strIP, strHWID, uGameCode, uLocale, null);
		}

		public void AuthorizedLogin64Async(byte uSecureCode, string strNexonID, long nNexonSN64, string strIP, string strHWID, uint uGameCode, uint uLocale, object userState)
		{
			if (this.AuthorizedLogin64OperationCompleted == null)
			{
				this.AuthorizedLogin64OperationCompleted = new SendOrPostCallback(this.OnAuthorizedLogin64OperationCompleted);
			}
			base.InvokeAsync("AuthorizedLogin64", new object[]
			{
				uSecureCode,
				strNexonID,
				nNexonSN64,
				strIP,
				strHWID,
				uGameCode,
				uLocale
			}, this.AuthorizedLogin64OperationCompleted, userState);
		}

		private void OnAuthorizedLogin64OperationCompleted(object arg)
		{
			if (this.AuthorizedLogin64Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AuthorizedLogin64Completed(this, new AuthorizedLogin64CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/ChannelingLogin", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ChannelingLoginResult ChannelingLogin(string strNexonID, int nNexonSN, string strIP, uint uGameCode, uint uLocale)
		{
			object[] array = base.Invoke("ChannelingLogin", new object[]
			{
				strNexonID,
				nNexonSN,
				strIP,
				uGameCode,
				uLocale
			});
			return (ChannelingLoginResult)array[0];
		}

		public void ChannelingLoginAsync(string strNexonID, int nNexonSN, string strIP, uint uGameCode, uint uLocale)
		{
			this.ChannelingLoginAsync(strNexonID, nNexonSN, strIP, uGameCode, uLocale, null);
		}

		public void ChannelingLoginAsync(string strNexonID, int nNexonSN, string strIP, uint uGameCode, uint uLocale, object userState)
		{
			if (this.ChannelingLoginOperationCompleted == null)
			{
				this.ChannelingLoginOperationCompleted = new SendOrPostCallback(this.OnChannelingLoginOperationCompleted);
			}
			base.InvokeAsync("ChannelingLogin", new object[]
			{
				strNexonID,
				nNexonSN,
				strIP,
				uGameCode,
				uLocale
			}, this.ChannelingLoginOperationCompleted, userState);
		}

		private void OnChannelingLoginOperationCompleted(object arg)
		{
			if (this.ChannelingLoginCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ChannelingLoginCompleted(this, new ChannelingLoginCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/MobileLogin", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public MobileLoginResult MobileLogin(string strNexonID, string strPwd, string strIP)
		{
			object[] array = base.Invoke("MobileLogin", new object[]
			{
				strNexonID,
				strPwd,
				strIP
			});
			return (MobileLoginResult)array[0];
		}

		public void MobileLoginAsync(string strNexonID, string strPwd, string strIP)
		{
			this.MobileLoginAsync(strNexonID, strPwd, strIP, null);
		}

		public void MobileLoginAsync(string strNexonID, string strPwd, string strIP, object userState)
		{
			if (this.MobileLoginOperationCompleted == null)
			{
				this.MobileLoginOperationCompleted = new SendOrPostCallback(this.OnMobileLoginOperationCompleted);
			}
			base.InvokeAsync("MobileLogin", new object[]
			{
				strNexonID,
				strPwd,
				strIP
			}, this.MobileLoginOperationCompleted, userState);
		}

		private void OnMobileLoginOperationCompleted(object arg)
		{
			if (this.MobileLoginCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MobileLoginCompleted(this, new MobileLoginCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/PCBangLogin", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public PCBangLoginResult PCBangLogin(string strOTP, string strIP)
		{
			object[] array = base.Invoke("PCBangLogin", new object[]
			{
				strOTP,
				strIP
			});
			return (PCBangLoginResult)array[0];
		}

		public void PCBangLoginAsync(string strOTP, string strIP)
		{
			this.PCBangLoginAsync(strOTP, strIP, null);
		}

		public void PCBangLoginAsync(string strOTP, string strIP, object userState)
		{
			if (this.PCBangLoginOperationCompleted == null)
			{
				this.PCBangLoginOperationCompleted = new SendOrPostCallback(this.OnPCBangLoginOperationCompleted);
			}
			base.InvokeAsync("PCBangLogin", new object[]
			{
				strOTP,
				strIP
			}, this.PCBangLoginOperationCompleted, userState);
		}

		private void OnPCBangLoginOperationCompleted(object arg)
		{
			if (this.PCBangLoginCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PCBangLoginCompleted(this, new PCBangLoginCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/Logout", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public LogoutResult Logout(string strPassport, string strIP)
		{
			object[] array = base.Invoke("Logout", new object[]
			{
				strPassport,
				strIP
			});
			return (LogoutResult)array[0];
		}

		public void LogoutAsync(string strPassport, string strIP)
		{
			this.LogoutAsync(strPassport, strIP, null);
		}

		public void LogoutAsync(string strPassport, string strIP, object userState)
		{
			if (this.LogoutOperationCompleted == null)
			{
				this.LogoutOperationCompleted = new SendOrPostCallback(this.OnLogoutOperationCompleted);
			}
			base.InvokeAsync("Logout", new object[]
			{
				strPassport,
				strIP
			}, this.LogoutOperationCompleted, userState);
		}

		private void OnLogoutOperationCompleted(object arg)
		{
			if (this.LogoutCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LogoutCompleted(this, new LogoutCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/AttachSession", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public AttachSessionResult AttachSession(string strPassport, string strIP)
		{
			object[] array = base.Invoke("AttachSession", new object[]
			{
				strPassport,
				strIP
			});
			return (AttachSessionResult)array[0];
		}

		public void AttachSessionAsync(string strPassport, string strIP)
		{
			this.AttachSessionAsync(strPassport, strIP, null);
		}

		public void AttachSessionAsync(string strPassport, string strIP, object userState)
		{
			if (this.AttachSessionOperationCompleted == null)
			{
				this.AttachSessionOperationCompleted = new SendOrPostCallback(this.OnAttachSessionOperationCompleted);
			}
			base.InvokeAsync("AttachSession", new object[]
			{
				strPassport,
				strIP
			}, this.AttachSessionOperationCompleted, userState);
		}

		private void OnAttachSessionOperationCompleted(object arg)
		{
			if (this.AttachSessionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AttachSessionCompleted(this, new AttachSessionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UpdateSession", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public UpdateSessionResult UpdateSession(string strPassport, string strIP)
		{
			object[] array = base.Invoke("UpdateSession", new object[]
			{
				strPassport,
				strIP
			});
			return (UpdateSessionResult)array[0];
		}

		public void UpdateSessionAsync(string strPassport, string strIP)
		{
			this.UpdateSessionAsync(strPassport, strIP, null);
		}

		public void UpdateSessionAsync(string strPassport, string strIP, object userState)
		{
			if (this.UpdateSessionOperationCompleted == null)
			{
				this.UpdateSessionOperationCompleted = new SendOrPostCallback(this.OnUpdateSessionOperationCompleted);
			}
			base.InvokeAsync("UpdateSession", new object[]
			{
				strPassport,
				strIP
			}, this.UpdateSessionOperationCompleted, userState);
		}

		private void OnUpdateSessionOperationCompleted(object arg)
		{
			if (this.UpdateSessionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateSessionCompleted(this, new UpdateSessionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckSession", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CheckSessionResult CheckSession(string strPassport, string strIP)
		{
			object[] array = base.Invoke("CheckSession", new object[]
			{
				strPassport,
				strIP
			});
			return (CheckSessionResult)array[0];
		}

		public void CheckSessionAsync(string strPassport, string strIP)
		{
			this.CheckSessionAsync(strPassport, strIP, null);
		}

		public void CheckSessionAsync(string strPassport, string strIP, object userState)
		{
			if (this.CheckSessionOperationCompleted == null)
			{
				this.CheckSessionOperationCompleted = new SendOrPostCallback(this.OnCheckSessionOperationCompleted);
			}
			base.InvokeAsync("CheckSession", new object[]
			{
				strPassport,
				strIP
			}, this.CheckSessionOperationCompleted, userState);
		}

		private void OnCheckSessionOperationCompleted(object arg)
		{
			if (this.CheckSessionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckSessionCompleted(this, new CheckSessionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckSession2", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CheckSession2Result CheckSession2(string strPassport, string strIP, bool bAttach)
		{
			object[] array = base.Invoke("CheckSession2", new object[]
			{
				strPassport,
				strIP,
				bAttach
			});
			return (CheckSession2Result)array[0];
		}

		public void CheckSession2Async(string strPassport, string strIP, bool bAttach)
		{
			this.CheckSession2Async(strPassport, strIP, bAttach, null);
		}

		public void CheckSession2Async(string strPassport, string strIP, bool bAttach, object userState)
		{
			if (this.CheckSession2OperationCompleted == null)
			{
				this.CheckSession2OperationCompleted = new SendOrPostCallback(this.OnCheckSession2OperationCompleted);
			}
			base.InvokeAsync("CheckSession2", new object[]
			{
				strPassport,
				strIP,
				bAttach
			}, this.CheckSession2OperationCompleted, userState);
		}

		private void OnCheckSession2OperationCompleted(object arg)
		{
			if (this.CheckSession2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckSession2Completed(this, new CheckSession2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckAndAttachSession", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CheckAndAttachSessionResult CheckAndAttachSession(string strPassport, string strIP)
		{
			object[] array = base.Invoke("CheckAndAttachSession", new object[]
			{
				strPassport,
				strIP
			});
			return (CheckAndAttachSessionResult)array[0];
		}

		public void CheckAndAttachSessionAsync(string strPassport, string strIP)
		{
			this.CheckAndAttachSessionAsync(strPassport, strIP, null);
		}

		public void CheckAndAttachSessionAsync(string strPassport, string strIP, object userState)
		{
			if (this.CheckAndAttachSessionOperationCompleted == null)
			{
				this.CheckAndAttachSessionOperationCompleted = new SendOrPostCallback(this.OnCheckAndAttachSessionOperationCompleted);
			}
			base.InvokeAsync("CheckAndAttachSession", new object[]
			{
				strPassport,
				strIP
			}, this.CheckAndAttachSessionOperationCompleted, userState);
		}

		private void OnCheckAndAttachSessionOperationCompleted(object arg)
		{
			if (this.CheckAndAttachSessionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckAndAttachSessionCompleted(this, new CheckAndAttachSessionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckSessionAndUpdateInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CheckSessionResult CheckSessionAndUpdateInfo(string strPassport, string strIP, int uUpdateInfoType, int nArg0)
		{
			object[] array = base.Invoke("CheckSessionAndUpdateInfo", new object[]
			{
				strPassport,
				strIP,
				uUpdateInfoType,
				nArg0
			});
			return (CheckSessionResult)array[0];
		}

		public void CheckSessionAndUpdateInfoAsync(string strPassport, string strIP, int uUpdateInfoType, int nArg0)
		{
			this.CheckSessionAndUpdateInfoAsync(strPassport, strIP, uUpdateInfoType, nArg0, null);
		}

		public void CheckSessionAndUpdateInfoAsync(string strPassport, string strIP, int uUpdateInfoType, int nArg0, object userState)
		{
			if (this.CheckSessionAndUpdateInfoOperationCompleted == null)
			{
				this.CheckSessionAndUpdateInfoOperationCompleted = new SendOrPostCallback(this.OnCheckSessionAndUpdateInfoOperationCompleted);
			}
			base.InvokeAsync("CheckSessionAndUpdateInfo", new object[]
			{
				strPassport,
				strIP,
				uUpdateInfoType,
				nArg0
			}, this.CheckSessionAndUpdateInfoOperationCompleted, userState);
		}

		private void OnCheckSessionAndUpdateInfoOperationCompleted(object arg)
		{
			if (this.CheckSessionAndUpdateInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckSessionAndUpdateInfoCompleted(this, new CheckSessionAndUpdateInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckSessionAndGetInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CheckSessionAndGetInfoResult CheckSessionAndGetInfo(string strPassport, string strIP)
		{
			object[] array = base.Invoke("CheckSessionAndGetInfo", new object[]
			{
				strPassport,
				strIP
			});
			return (CheckSessionAndGetInfoResult)array[0];
		}

		public void CheckSessionAndGetInfoAsync(string strPassport, string strIP)
		{
			this.CheckSessionAndGetInfoAsync(strPassport, strIP, null);
		}

		public void CheckSessionAndGetInfoAsync(string strPassport, string strIP, object userState)
		{
			if (this.CheckSessionAndGetInfoOperationCompleted == null)
			{
				this.CheckSessionAndGetInfoOperationCompleted = new SendOrPostCallback(this.OnCheckSessionAndGetInfoOperationCompleted);
			}
			base.InvokeAsync("CheckSessionAndGetInfo", new object[]
			{
				strPassport,
				strIP
			}, this.CheckSessionAndGetInfoOperationCompleted, userState);
		}

		private void OnCheckSessionAndGetInfoOperationCompleted(object arg)
		{
			if (this.CheckSessionAndGetInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckSessionAndGetInfoCompleted(this, new CheckSessionAndGetInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckSessionAndGetInfo2", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CheckSessionAndGetInfo2Result CheckSessionAndGetInfo2(string strPassport, string strIP, string strHWID, uint uGameCode)
		{
			object[] array = base.Invoke("CheckSessionAndGetInfo2", new object[]
			{
				strPassport,
				strIP,
				strHWID,
				uGameCode
			});
			return (CheckSessionAndGetInfo2Result)array[0];
		}

		public void CheckSessionAndGetInfo2Async(string strPassport, string strIP, string strHWID, uint uGameCode)
		{
			this.CheckSessionAndGetInfo2Async(strPassport, strIP, strHWID, uGameCode, null);
		}

		public void CheckSessionAndGetInfo2Async(string strPassport, string strIP, string strHWID, uint uGameCode, object userState)
		{
			if (this.CheckSessionAndGetInfo2OperationCompleted == null)
			{
				this.CheckSessionAndGetInfo2OperationCompleted = new SendOrPostCallback(this.OnCheckSessionAndGetInfo2OperationCompleted);
			}
			base.InvokeAsync("CheckSessionAndGetInfo2", new object[]
			{
				strPassport,
				strIP,
				strHWID,
				uGameCode
			}, this.CheckSessionAndGetInfo2OperationCompleted, userState);
		}

		private void OnCheckSessionAndGetInfo2OperationCompleted(object arg)
		{
			if (this.CheckSessionAndGetInfo2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckSessionAndGetInfo2Completed(this, new CheckSessionAndGetInfo2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckSessionAndGetInfo3", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CheckSessionAndGetInfo3Result CheckSessionAndGetInfo3(string strPassport, string strIP, string strHWID, uint uGameCode)
		{
			object[] array = base.Invoke("CheckSessionAndGetInfo3", new object[]
			{
				strPassport,
				strIP,
				strHWID,
				uGameCode
			});
			return (CheckSessionAndGetInfo3Result)array[0];
		}

		public void CheckSessionAndGetInfo3Async(string strPassport, string strIP, string strHWID, uint uGameCode)
		{
			this.CheckSessionAndGetInfo3Async(strPassport, strIP, strHWID, uGameCode, null);
		}

		public void CheckSessionAndGetInfo3Async(string strPassport, string strIP, string strHWID, uint uGameCode, object userState)
		{
			if (this.CheckSessionAndGetInfo3OperationCompleted == null)
			{
				this.CheckSessionAndGetInfo3OperationCompleted = new SendOrPostCallback(this.OnCheckSessionAndGetInfo3OperationCompleted);
			}
			base.InvokeAsync("CheckSessionAndGetInfo3", new object[]
			{
				strPassport,
				strIP,
				strHWID,
				uGameCode
			}, this.CheckSessionAndGetInfo3OperationCompleted, userState);
		}

		private void OnCheckSessionAndGetInfo3OperationCompleted(object arg)
		{
			if (this.CheckSessionAndGetInfo3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckSessionAndGetInfo3Completed(this, new CheckSessionAndGetInfo3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckSessionAndGetInfo4", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CheckSessionAndGetInfo4Result CheckSessionAndGetInfo4(string strPassport, string strIP, string strHWID, uint uGameCode)
		{
			object[] array = base.Invoke("CheckSessionAndGetInfo4", new object[]
			{
				strPassport,
				strIP,
				strHWID,
				uGameCode
			});
			return (CheckSessionAndGetInfo4Result)array[0];
		}

		public void CheckSessionAndGetInfo4Async(string strPassport, string strIP, string strHWID, uint uGameCode)
		{
			this.CheckSessionAndGetInfo4Async(strPassport, strIP, strHWID, uGameCode, null);
		}

		public void CheckSessionAndGetInfo4Async(string strPassport, string strIP, string strHWID, uint uGameCode, object userState)
		{
			if (this.CheckSessionAndGetInfo4OperationCompleted == null)
			{
				this.CheckSessionAndGetInfo4OperationCompleted = new SendOrPostCallback(this.OnCheckSessionAndGetInfo4OperationCompleted);
			}
			base.InvokeAsync("CheckSessionAndGetInfo4", new object[]
			{
				strPassport,
				strIP,
				strHWID,
				uGameCode
			}, this.CheckSessionAndGetInfo4OperationCompleted, userState);
		}

		private void OnCheckSessionAndGetInfo4OperationCompleted(object arg)
		{
			if (this.CheckSessionAndGetInfo4Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckSessionAndGetInfo4Completed(this, new CheckSessionAndGetInfo4CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckIDnPassword", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CheckIDnPasswordResult CheckIDnPassword(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale)
		{
			object[] array = base.Invoke("CheckIDnPassword", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale
			});
			return (CheckIDnPasswordResult)array[0];
		}

		public void CheckIDnPasswordAsync(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale)
		{
			this.CheckIDnPasswordAsync(strNexonID, strPwd, strIP, uGameCode, uLocale, null);
		}

		public void CheckIDnPasswordAsync(string strNexonID, string strPwd, string strIP, uint uGameCode, uint uLocale, object userState)
		{
			if (this.CheckIDnPasswordOperationCompleted == null)
			{
				this.CheckIDnPasswordOperationCompleted = new SendOrPostCallback(this.OnCheckIDnPasswordOperationCompleted);
			}
			base.InvokeAsync("CheckIDnPassword", new object[]
			{
				strNexonID,
				strPwd,
				strIP,
				uGameCode,
				uLocale
			}, this.CheckIDnPasswordOperationCompleted, userState);
		}

		private void OnCheckIDnPasswordOperationCompleted(object arg)
		{
			if (this.CheckIDnPasswordCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckIDnPasswordCompleted(this, new CheckIDnPasswordCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UpdateInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public UpdateInfoResult UpdateInfo(int nNexonSN, int uUpdateInfoType, int nArg0)
		{
			object[] array = base.Invoke("UpdateInfo", new object[]
			{
				nNexonSN,
				uUpdateInfoType,
				nArg0
			});
			return (UpdateInfoResult)array[0];
		}

		public void UpdateInfoAsync(int nNexonSN, int uUpdateInfoType, int nArg0)
		{
			this.UpdateInfoAsync(nNexonSN, uUpdateInfoType, nArg0, null);
		}

		public void UpdateInfoAsync(int nNexonSN, int uUpdateInfoType, int nArg0, object userState)
		{
			if (this.UpdateInfoOperationCompleted == null)
			{
				this.UpdateInfoOperationCompleted = new SendOrPostCallback(this.OnUpdateInfoOperationCompleted);
			}
			base.InvokeAsync("UpdateInfo", new object[]
			{
				nNexonSN,
				uUpdateInfoType,
				nArg0
			}, this.UpdateInfoOperationCompleted, userState);
		}

		private void OnUpdateInfoOperationCompleted(object arg)
		{
			if (this.UpdateInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateInfoCompleted(this, new UpdateInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UpdateInfo64", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public UpdateInfoResult UpdateInfo64(long nNexonSN64, int uUpdateInfoType, int nArg0)
		{
			object[] array = base.Invoke("UpdateInfo64", new object[]
			{
				nNexonSN64,
				uUpdateInfoType,
				nArg0
			});
			return (UpdateInfoResult)array[0];
		}

		public void UpdateInfo64Async(long nNexonSN64, int uUpdateInfoType, int nArg0)
		{
			this.UpdateInfo64Async(nNexonSN64, uUpdateInfoType, nArg0, null);
		}

		public void UpdateInfo64Async(long nNexonSN64, int uUpdateInfoType, int nArg0, object userState)
		{
			if (this.UpdateInfo64OperationCompleted == null)
			{
				this.UpdateInfo64OperationCompleted = new SendOrPostCallback(this.OnUpdateInfo64OperationCompleted);
			}
			base.InvokeAsync("UpdateInfo64", new object[]
			{
				nNexonSN64,
				uUpdateInfoType,
				nArg0
			}, this.UpdateInfo64OperationCompleted, userState);
		}

		private void OnUpdateInfo64OperationCompleted(object arg)
		{
			if (this.UpdateInfo64Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateInfo64Completed(this, new UpdateInfo64CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetOldFashionInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public GetOldFashionInfoResult GetOldFashionInfo(string strPassport, string strIP)
		{
			object[] array = base.Invoke("GetOldFashionInfo", new object[]
			{
				strPassport,
				strIP
			});
			return (GetOldFashionInfoResult)array[0];
		}

		public void GetOldFashionInfoAsync(string strPassport, string strIP)
		{
			this.GetOldFashionInfoAsync(strPassport, strIP, null);
		}

		public void GetOldFashionInfoAsync(string strPassport, string strIP, object userState)
		{
			if (this.GetOldFashionInfoOperationCompleted == null)
			{
				this.GetOldFashionInfoOperationCompleted = new SendOrPostCallback(this.OnGetOldFashionInfoOperationCompleted);
			}
			base.InvokeAsync("GetOldFashionInfo", new object[]
			{
				strPassport,
				strIP
			}, this.GetOldFashionInfoOperationCompleted, userState);
		}

		private void OnGetOldFashionInfoOperationCompleted(object arg)
		{
			if (this.GetOldFashionInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetOldFashionInfoCompleted(this, new GetOldFashionInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/UpgradeGameToken", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public UpgradeGameTokenResult UpgradeGameToken(byte uSecureCode, string strConPwd, uint uGameCode, string strPassport, string strHWID, string strIP)
		{
			object[] array = base.Invoke("UpgradeGameToken", new object[]
			{
				uSecureCode,
				strConPwd,
				uGameCode,
				strPassport,
				strHWID,
				strIP
			});
			return (UpgradeGameTokenResult)array[0];
		}

		public void UpgradeGameTokenAsync(byte uSecureCode, string strConPwd, uint uGameCode, string strPassport, string strHWID, string strIP)
		{
			this.UpgradeGameTokenAsync(uSecureCode, strConPwd, uGameCode, strPassport, strHWID, strIP, null);
		}

		public void UpgradeGameTokenAsync(byte uSecureCode, string strConPwd, uint uGameCode, string strPassport, string strHWID, string strIP, object userState)
		{
			if (this.UpgradeGameTokenOperationCompleted == null)
			{
				this.UpgradeGameTokenOperationCompleted = new SendOrPostCallback(this.OnUpgradeGameTokenOperationCompleted);
			}
			base.InvokeAsync("UpgradeGameToken", new object[]
			{
				uSecureCode,
				strConPwd,
				uGameCode,
				strPassport,
				strHWID,
				strIP
			}, this.UpgradeGameTokenOperationCompleted, userState);
		}

		private void OnUpgradeGameTokenOperationCompleted(object arg)
		{
			if (this.UpgradeGameTokenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpgradeGameTokenCompleted(this, new UpgradeGameTokenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/Ban", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public BanResult Ban(long nNexonSN)
		{
			object[] array = base.Invoke("Ban", new object[]
			{
				nNexonSN
			});
			return (BanResult)array[0];
		}

		public void BanAsync(long nNexonSN)
		{
			this.BanAsync(nNexonSN, null);
		}

		public void BanAsync(long nNexonSN, object userState)
		{
			if (this.BanOperationCompleted == null)
			{
				this.BanOperationCompleted = new SendOrPostCallback(this.OnBanOperationCompleted);
			}
			base.InvokeAsync("Ban", new object[]
			{
				nNexonSN
			}, this.BanOperationCompleted, userState);
		}

		private void OnBanOperationCompleted(object arg)
		{
			if (this.BanCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.BanCompleted(this, new BanCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/IsOTPUsable", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public IsOTPUsableResult IsOTPUsable(int nNexonSN)
		{
			object[] array = base.Invoke("IsOTPUsable", new object[]
			{
				nNexonSN
			});
			return (IsOTPUsableResult)array[0];
		}

		public void IsOTPUsableAsync(int nNexonSN)
		{
			this.IsOTPUsableAsync(nNexonSN, null);
		}

		public void IsOTPUsableAsync(int nNexonSN, object userState)
		{
			if (this.IsOTPUsableOperationCompleted == null)
			{
				this.IsOTPUsableOperationCompleted = new SendOrPostCallback(this.OnIsOTPUsableOperationCompleted);
			}
			base.InvokeAsync("IsOTPUsable", new object[]
			{
				nNexonSN
			}, this.IsOTPUsableOperationCompleted, userState);
		}

		private void OnIsOTPUsableOperationCompleted(object arg)
		{
			if (this.IsOTPUsableCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.IsOTPUsableCompleted(this, new IsOTPUsableCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/IsOTPUsable64", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public IsOTPUsableResult IsOTPUsable64(long nNexonSN64)
		{
			object[] array = base.Invoke("IsOTPUsable64", new object[]
			{
				nNexonSN64
			});
			return (IsOTPUsableResult)array[0];
		}

		public void IsOTPUsable64Async(long nNexonSN64)
		{
			this.IsOTPUsable64Async(nNexonSN64, null);
		}

		public void IsOTPUsable64Async(long nNexonSN64, object userState)
		{
			if (this.IsOTPUsable64OperationCompleted == null)
			{
				this.IsOTPUsable64OperationCompleted = new SendOrPostCallback(this.OnIsOTPUsable64OperationCompleted);
			}
			base.InvokeAsync("IsOTPUsable64", new object[]
			{
				nNexonSN64
			}, this.IsOTPUsable64OperationCompleted, userState);
		}

		private void OnIsOTPUsable64OperationCompleted(object arg)
		{
			if (this.IsOTPUsable64Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.IsOTPUsable64Completed(this, new IsOTPUsable64CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/IsLogin", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public IsLoginResult IsLogin(long nNexonSN)
		{
			object[] array = base.Invoke("IsLogin", new object[]
			{
				nNexonSN
			});
			return (IsLoginResult)array[0];
		}

		public void IsLoginAsync(long nNexonSN)
		{
			this.IsLoginAsync(nNexonSN, null);
		}

		public void IsLoginAsync(long nNexonSN, object userState)
		{
			if (this.IsLoginOperationCompleted == null)
			{
				this.IsLoginOperationCompleted = new SendOrPostCallback(this.OnIsLoginOperationCompleted);
			}
			base.InvokeAsync("IsLogin", new object[]
			{
				nNexonSN
			}, this.IsLoginOperationCompleted, userState);
		}

		private void OnIsLoginOperationCompleted(object arg)
		{
			if (this.IsLoginCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.IsLoginCompleted(this, new IsLoginCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/DualTestLogin", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool DualTestLogin(string strFunctionName, uint uGameCode, string strNexonID, long nNexonSN, string strIP, byte uChannelCode, string strChannelUID, long nSecurityState, bool bNewMembership, byte nMainAuthLevel, byte nSubAuthLevel)
		{
			object[] array = base.Invoke("DualTestLogin", new object[]
			{
				strFunctionName,
				uGameCode,
				strNexonID,
				nNexonSN,
				strIP,
				uChannelCode,
				strChannelUID,
				nSecurityState,
				bNewMembership,
				nMainAuthLevel,
				nSubAuthLevel
			});
			return (bool)array[0];
		}

		public void DualTestLoginAsync(string strFunctionName, uint uGameCode, string strNexonID, long nNexonSN, string strIP, byte uChannelCode, string strChannelUID, long nSecurityState, bool bNewMembership, byte nMainAuthLevel, byte nSubAuthLevel)
		{
			this.DualTestLoginAsync(strFunctionName, uGameCode, strNexonID, nNexonSN, strIP, uChannelCode, strChannelUID, nSecurityState, bNewMembership, nMainAuthLevel, nSubAuthLevel, null);
		}

		public void DualTestLoginAsync(string strFunctionName, uint uGameCode, string strNexonID, long nNexonSN, string strIP, byte uChannelCode, string strChannelUID, long nSecurityState, bool bNewMembership, byte nMainAuthLevel, byte nSubAuthLevel, object userState)
		{
			if (this.DualTestLoginOperationCompleted == null)
			{
				this.DualTestLoginOperationCompleted = new SendOrPostCallback(this.OnDualTestLoginOperationCompleted);
			}
			base.InvokeAsync("DualTestLogin", new object[]
			{
				strFunctionName,
				uGameCode,
				strNexonID,
				nNexonSN,
				strIP,
				uChannelCode,
				strChannelUID,
				nSecurityState,
				bNewMembership,
				nMainAuthLevel,
				nSubAuthLevel
			}, this.DualTestLoginOperationCompleted, userState);
		}

		private void OnDualTestLoginOperationCompleted(object arg)
		{
			if (this.DualTestLoginCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DualTestLoginCompleted(this, new DualTestLoginCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/DualTestLogout", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool DualTestLogout(uint uGameCode, string strNexonID, long nNexonSN, string strPassportIP, ulong uSessionKey)
		{
			object[] array = base.Invoke("DualTestLogout", new object[]
			{
				uGameCode,
				strNexonID,
				nNexonSN,
				strPassportIP,
				uSessionKey
			});
			return (bool)array[0];
		}

		public void DualTestLogoutAsync(uint uGameCode, string strNexonID, long nNexonSN, string strPassportIP, ulong uSessionKey)
		{
			this.DualTestLogoutAsync(uGameCode, strNexonID, nNexonSN, strPassportIP, uSessionKey, null);
		}

		public void DualTestLogoutAsync(uint uGameCode, string strNexonID, long nNexonSN, string strPassportIP, ulong uSessionKey, object userState)
		{
			if (this.DualTestLogoutOperationCompleted == null)
			{
				this.DualTestLogoutOperationCompleted = new SendOrPostCallback(this.OnDualTestLogoutOperationCompleted);
			}
			base.InvokeAsync("DualTestLogout", new object[]
			{
				uGameCode,
				strNexonID,
				nNexonSN,
				strPassportIP,
				uSessionKey
			}, this.DualTestLogoutOperationCompleted, userState);
		}

		private void OnDualTestLogoutOperationCompleted(object arg)
		{
			if (this.DualTestLogoutCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DualTestLogoutCompleted(this, new DualTestLogoutCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/DualTestCheckSession", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool DualTestCheckSession(uint uGameCode, string strNexonID, long nNexonSN, string strPassportIP, byte uChannelCode, ulong uSessionKey)
		{
			object[] array = base.Invoke("DualTestCheckSession", new object[]
			{
				uGameCode,
				strNexonID,
				nNexonSN,
				strPassportIP,
				uChannelCode,
				uSessionKey
			});
			return (bool)array[0];
		}

		public void DualTestCheckSessionAsync(uint uGameCode, string strNexonID, long nNexonSN, string strPassportIP, byte uChannelCode, ulong uSessionKey)
		{
			this.DualTestCheckSessionAsync(uGameCode, strNexonID, nNexonSN, strPassportIP, uChannelCode, uSessionKey, null);
		}

		public void DualTestCheckSessionAsync(uint uGameCode, string strNexonID, long nNexonSN, string strPassportIP, byte uChannelCode, ulong uSessionKey, object userState)
		{
			if (this.DualTestCheckSessionOperationCompleted == null)
			{
				this.DualTestCheckSessionOperationCompleted = new SendOrPostCallback(this.OnDualTestCheckSessionOperationCompleted);
			}
			base.InvokeAsync("DualTestCheckSession", new object[]
			{
				uGameCode,
				strNexonID,
				nNexonSN,
				strPassportIP,
				uChannelCode,
				uSessionKey
			}, this.DualTestCheckSessionOperationCompleted, userState);
		}

		private void OnDualTestCheckSessionOperationCompleted(object arg)
		{
			if (this.DualTestCheckSessionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DualTestCheckSessionCompleted(this, new DualTestCheckSessionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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

		private SendOrPostCallback LoginOperationCompleted;

		private SendOrPostCallback Login2OperationCompleted;

		private SendOrPostCallback Login3OperationCompleted;

		private SendOrPostCallback Login4OperationCompleted;

		private SendOrPostCallback AuthorizedLogin64OperationCompleted;

		private SendOrPostCallback ChannelingLoginOperationCompleted;

		private SendOrPostCallback MobileLoginOperationCompleted;

		private SendOrPostCallback PCBangLoginOperationCompleted;

		private SendOrPostCallback LogoutOperationCompleted;

		private SendOrPostCallback AttachSessionOperationCompleted;

		private SendOrPostCallback UpdateSessionOperationCompleted;

		private SendOrPostCallback CheckSessionOperationCompleted;

		private SendOrPostCallback CheckSession2OperationCompleted;

		private SendOrPostCallback CheckAndAttachSessionOperationCompleted;

		private SendOrPostCallback CheckSessionAndUpdateInfoOperationCompleted;

		private SendOrPostCallback CheckSessionAndGetInfoOperationCompleted;

		private SendOrPostCallback CheckSessionAndGetInfo2OperationCompleted;

		private SendOrPostCallback CheckSessionAndGetInfo3OperationCompleted;

		private SendOrPostCallback CheckSessionAndGetInfo4OperationCompleted;

		private SendOrPostCallback CheckIDnPasswordOperationCompleted;

		private SendOrPostCallback UpdateInfoOperationCompleted;

		private SendOrPostCallback UpdateInfo64OperationCompleted;

		private SendOrPostCallback GetOldFashionInfoOperationCompleted;

		private SendOrPostCallback UpgradeGameTokenOperationCompleted;

		private SendOrPostCallback BanOperationCompleted;

		private SendOrPostCallback IsOTPUsableOperationCompleted;

		private SendOrPostCallback IsOTPUsable64OperationCompleted;

		private SendOrPostCallback IsLoginOperationCompleted;

		private SendOrPostCallback DualTestLoginOperationCompleted;

		private SendOrPostCallback DualTestLogoutOperationCompleted;

		private SendOrPostCallback DualTestCheckSessionOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
