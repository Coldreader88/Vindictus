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

namespace Nexon.Com.UserWrapper.UserAPI.Warning
{
	[WebServiceBinding(Name = "warningSoap", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	public class warning : SoapHttpClientProtocol
	{
		public warning()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_UserAPI_Warning_warning;
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

		public event AddWarningInfoCompletedEventHandler AddWarningInfoCompleted;

		public event GetWarningInfoCompletedEventHandler GetWarningInfoCompleted;

		public event GetWarningListCompletedEventHandler GetWarningListCompleted;

		public event GetWarningLogListCompletedEventHandler GetWarningLogListCompleted;

		[SoapDocumentMethod("http://tempuri.org/AddWarningInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int AddWarningInfo(int n4ServiceCode, int n4NexonSN, string strNexonID, int n4GameCode, byte n1WarningReasonCode, string strWarningReason, short n2WarningPoint, string strBackOfficeUser, int n4ReportSN)
		{
			object[] array = base.Invoke("AddWarningInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				n4GameCode,
				n1WarningReasonCode,
				strWarningReason,
				n2WarningPoint,
				strBackOfficeUser,
				n4ReportSN
			});
			return (int)array[0];
		}

		public void AddWarningInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, int n4GameCode, byte n1WarningReasonCode, string strWarningReason, short n2WarningPoint, string strBackOfficeUser, int n4ReportSN)
		{
			this.AddWarningInfoAsync(n4ServiceCode, n4NexonSN, strNexonID, n4GameCode, n1WarningReasonCode, strWarningReason, n2WarningPoint, strBackOfficeUser, n4ReportSN, null);
		}

		public void AddWarningInfoAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, int n4GameCode, byte n1WarningReasonCode, string strWarningReason, short n2WarningPoint, string strBackOfficeUser, int n4ReportSN, object userState)
		{
			if (this.AddWarningInfoOperationCompleted == null)
			{
				this.AddWarningInfoOperationCompleted = new SendOrPostCallback(this.OnAddWarningInfoOperationCompleted);
			}
			base.InvokeAsync("AddWarningInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				n4GameCode,
				n1WarningReasonCode,
				strWarningReason,
				n2WarningPoint,
				strBackOfficeUser,
				n4ReportSN
			}, this.AddWarningInfoOperationCompleted, userState);
		}

		private void OnAddWarningInfoOperationCompleted(object arg)
		{
			if (this.AddWarningInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddWarningInfoCompleted(this, new AddWarningInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetWarningInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetWarningInfo(int n4ServiceCode, int n4NexonSN, int n4GameCode, out string strXML)
		{
			object[] array = base.Invoke("GetWarningInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				n4GameCode
			});
			strXML = (string)array[1];
			return (int)array[0];
		}

		public void GetWarningInfoAsync(int n4ServiceCode, int n4NexonSN, int n4GameCode)
		{
			this.GetWarningInfoAsync(n4ServiceCode, n4NexonSN, n4GameCode, null);
		}

		public void GetWarningInfoAsync(int n4ServiceCode, int n4NexonSN, int n4GameCode, object userState)
		{
			if (this.GetWarningInfoOperationCompleted == null)
			{
				this.GetWarningInfoOperationCompleted = new SendOrPostCallback(this.OnGetWarningInfoOperationCompleted);
			}
			base.InvokeAsync("GetWarningInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				n4GameCode
			}, this.GetWarningInfoOperationCompleted, userState);
		}

		private void OnGetWarningInfoOperationCompleted(object arg)
		{
			if (this.GetWarningInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetWarningInfoCompleted(this, new GetWarningInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetWarningList", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetWarningList(int n4ServiceCode, int n4NexonSN, string strNexonID, bool isPenaltyMode, out int n4TotalRowCount, out DataSet dsWarningList)
		{
			object[] array = base.Invoke("GetWarningList", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				isPenaltyMode
			});
			n4TotalRowCount = (int)array[1];
			dsWarningList = (DataSet)array[2];
			return (int)array[0];
		}

		public void GetWarningListAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, bool isPenaltyMode)
		{
			this.GetWarningListAsync(n4ServiceCode, n4NexonSN, strNexonID, isPenaltyMode, null);
		}

		public void GetWarningListAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, bool isPenaltyMode, object userState)
		{
			if (this.GetWarningListOperationCompleted == null)
			{
				this.GetWarningListOperationCompleted = new SendOrPostCallback(this.OnGetWarningListOperationCompleted);
			}
			base.InvokeAsync("GetWarningList", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				isPenaltyMode
			}, this.GetWarningListOperationCompleted, userState);
		}

		private void OnGetWarningListOperationCompleted(object arg)
		{
			if (this.GetWarningListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetWarningListCompleted(this, new GetWarningListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetWarningLogList", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetWarningLogList(int n4ServiceCode, int n4NexonSN, string strNexonID, int n4GameCode, string strBackofficeUser, DateTime dtCreateDate_start, DateTime dtCreateDate_end, DateTime dtEndWarningDate_start, DateTime dtEndWarningDate_end, int n4PageNo, byte n1PageSize, out int n4TotalRowCount, out DataSet dsWarningLogList)
		{
			object[] array = base.Invoke("GetWarningLogList", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				n4GameCode,
				strBackofficeUser,
				dtCreateDate_start,
				dtCreateDate_end,
				dtEndWarningDate_start,
				dtEndWarningDate_end,
				n4PageNo,
				n1PageSize
			});
			n4TotalRowCount = (int)array[1];
			dsWarningLogList = (DataSet)array[2];
			return (int)array[0];
		}

		public void GetWarningLogListAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, int n4GameCode, string strBackofficeUser, DateTime dtCreateDate_start, DateTime dtCreateDate_end, DateTime dtEndWarningDate_start, DateTime dtEndWarningDate_end, int n4PageNo, byte n1PageSize)
		{
			this.GetWarningLogListAsync(n4ServiceCode, n4NexonSN, strNexonID, n4GameCode, strBackofficeUser, dtCreateDate_start, dtCreateDate_end, dtEndWarningDate_start, dtEndWarningDate_end, n4PageNo, n1PageSize, null);
		}

		public void GetWarningLogListAsync(int n4ServiceCode, int n4NexonSN, string strNexonID, int n4GameCode, string strBackofficeUser, DateTime dtCreateDate_start, DateTime dtCreateDate_end, DateTime dtEndWarningDate_start, DateTime dtEndWarningDate_end, int n4PageNo, byte n1PageSize, object userState)
		{
			if (this.GetWarningLogListOperationCompleted == null)
			{
				this.GetWarningLogListOperationCompleted = new SendOrPostCallback(this.OnGetWarningLogListOperationCompleted);
			}
			base.InvokeAsync("GetWarningLogList", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonID,
				n4GameCode,
				strBackofficeUser,
				dtCreateDate_start,
				dtCreateDate_end,
				dtEndWarningDate_start,
				dtEndWarningDate_end,
				n4PageNo,
				n1PageSize
			}, this.GetWarningLogListOperationCompleted, userState);
		}

		private void OnGetWarningLogListOperationCompleted(object arg)
		{
			if (this.GetWarningLogListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetWarningLogListCompleted(this, new GetWarningLogListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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

		private SendOrPostCallback AddWarningInfoOperationCompleted;

		private SendOrPostCallback GetWarningInfoOperationCompleted;

		private SendOrPostCallback GetWarningListOperationCompleted;

		private SendOrPostCallback GetWarningLogListOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
