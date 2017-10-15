using System;
using System.Net;
using System.Web.Services.Protocols;
using Nexon.Com.Log;

namespace Nexon.Com.DAO
{
	public abstract class SoapWrapperBase<TSoap, TResult> : DAOBase where TSoap : SoapHttpClientProtocol, new() where TResult : SoapResultBase, new()
	{
		protected abstract ServiceCode serviceCode { get; }

		public int CurrentRetryCount
		{
			get
			{
				return this._currentRetryCount;
			}
		}

		public SoapWrapperBase()
		{
		}

		private void Initialize()
		{
			if (this.SoapTimeout != 0)
			{
				TSoap soap = this.Soap;
				soap.Timeout = this.SoapTimeout;
			}
			if (this.WebServiceURLProvider != null && this.WebServiceURLProvider.GetURL() != null && this.WebServiceURLProvider.GetURL().Length != 0)
			{
				TSoap soap2 = this.Soap;
				soap2.Url = this.WebServiceURLProvider.GetURL();
			}
			if (this.WebServiceURLProvider != null && this.WebServiceURLProvider.GetProxyIP() != null && this.WebServiceURLProvider.GetProxyIP().Length != 0 && this.WebServiceURLProvider.GetProxyPort() != 0)
			{
				TSoap soap3 = this.Soap;
				soap3.Proxy = new WebProxy(this.WebServiceURLProvider.GetProxyIP(), this.WebServiceURLProvider.GetProxyPort());
			}
			this.InitializeSoap();
			this._isInitialized = true;
		}

		protected TSoap Soap
		{
			get
			{
				if (this._soap == null)
				{
					this._soap = Activator.CreateInstance<TSoap>();
				}
				return this._soap;
			}
		}

		protected TResult Result
		{
			get
			{
				if (this._result == null)
				{
					this._result = Activator.CreateInstance<TResult>();
				}
				return this._result;
			}
		}

		public TResult Execute()
		{
			if (!this._isInitialized)
			{
				this.Initialize();
			}
			Exception ex = null;
			int num = 0;
			int num2 = 0;
			int soapErrorCode = 0;
			string soapErrorMessage = null;
			try
			{
				do
				{
					try
					{
						num = System.Environment.TickCount;
						soapErrorCode = 0;
						soapErrorMessage = string.Empty;
						this.ExecuteImpl(out soapErrorCode, out soapErrorMessage);
						num2 += System.Environment.TickCount - num;
						break;
					}
					catch (Exception ex2)
					{
						int durationTime;
						num2 += (durationTime = System.Environment.TickCount - num);
						if (!(ex2 is WebException) || ((WebException)ex2).Status != WebExceptionStatus.Timeout)
						{
							throw ex2;
						}
						this.HandleTimeout(durationTime);
						if (this._currentRetryCount >= this.RetryCount)
						{
							throw new SoapTimeoutException(this._currentRetryCount, num2);
						}
					}
					this._currentRetryCount++;
				}
				while (this._currentRetryCount <= this.RetryCount);
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (this._soap != null)
				{
					try
					{
						this._soap.Dispose();
					}
					catch
					{
						try
						{
							this._soap.Abort();
						}
						catch
						{
						}
					}
					this._soap = default(TSoap);
				}
			}
			if (ex != null)
			{
				this.HandleSoapError(ex);
				throw ex;
			}
			TResult result = this.Result;
			result.SoapErrorCode = soapErrorCode;
			TResult result2 = this.Result;
			result2.SoapErrorMessage = soapErrorMessage;
			return this.Result;
		}

		protected abstract void ExecuteImpl(out int errorCode, out string errorMessage);

		protected virtual void InitializeSoap()
		{
		}

		protected virtual void HandleTimeout(int durationTime)
		{
		}

		protected virtual void HandleSoapError(Exception e)
		{
			try
			{
				string text;
				string text2;
				if (this.WebServiceURLProvider != null && this.WebServiceURLProvider.GetProxyIP() != null && this.WebServiceURLProvider.GetProxyIP().Length != 0 && this.WebServiceURLProvider.GetProxyPort() != 0)
				{
					text = this.WebServiceURLProvider.GetProxyIP();
					text2 = Convert.ToString(this.WebServiceURLProvider.GetProxyPort());
				}
				else
				{
					text = "N/A";
					text2 = "N/A";
				}
				TSoap soap = this.Soap;
				string url = soap.Url;
				if (this.WebServiceURLProvider != null && this.WebServiceURLProvider.GetURL() != null && this.WebServiceURLProvider.GetURL().Length != 0)
				{
					url = this.WebServiceURLProvider.GetURL();
				}
				string[] array = url.Split(new char[]
				{
					'/'
				});
				string arg = string.Empty;
				arg = ((array.Length > 0) ? array[array.Length - 1] : "Unknown");
				string strErrorMessage = string.Format("{0} : {1}", arg, e.Message);
				string text3 = string.Format("Message:{0}\r\n", e.Message);
				text3 += string.Format("T:{0:00000}|R:{1:00}|I:{2}|P:{3}\r\n", new object[]
				{
					this.SoapTimeout,
					this.RetryCount,
					text,
					text2
				});
				text3 += string.Format("Server:{0}\r\n", (array.Length > 2) ? array[array.Length - 2] : "Unknown");
				text3 += string.Format("Url:{0}\r\n", url);
				text3 += string.Format("Dump:{0}", e.StackTrace);
				int num;
				DateTime dateTime;
				ErrorLog.CreateErrorLog(this.serviceCode, 50000, null, strErrorMessage, text3, out num, out dateTime);
			}
			catch (Exception)
			{
			}
		}

		private bool _isInitialized;

		private int _currentRetryCount;

		protected string SoapName;

		protected int SoapTimeout = 3000;

		protected int RetryCount;

		protected IWebServiceURLProvider WebServiceURLProvider;

		private TSoap _soap = default(TSoap);

		private TResult _result = default(TResult);
	}
}
