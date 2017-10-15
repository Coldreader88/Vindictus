using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Nexon.Com.UserWrapper.Properties;

namespace Nexon.Com.UserWrapper.UserAPI.P2
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "p2Soap", Namespace = "http://tempuri.org/")]
	public class p2 : SoapHttpClientProtocol
	{
		public p2()
		{
			this.Url = Settings.Default.Nexon_Com_UserWrapper_UserAPI_P2_p2;
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

		public event GetUserInfoCompletedEventHandler GetUserInfoCompleted;

		public event GetUserWriteStatusCodeCompletedEventHandler GetUserWriteStatusCodeCompleted;

		public event GetUserNexonSN_ByNexonNameCompletedEventHandler GetUserNexonSN_ByNexonNameCompleted;

		public event GetUserNexonSN_ByNexonIDCompletedEventHandler GetUserNexonSN_ByNexonIDCompleted;

		public event CheckNexonNameCompletedEventHandler CheckNexonNameCompleted;

		public event ModifyNexonNameCompletedEventHandler ModifyNexonNameCompleted;

		public event ModifyOpenConfigureCompletedEventHandler ModifyOpenConfigureCompleted;

		public event ModifyRealBirthCompletedEventHandler ModifyRealBirthCompleted;

		public event ModifySchoolInfoCompletedEventHandler ModifySchoolInfoCompleted;

		public event ModifyMainPageCodeCompletedEventHandler ModifyMainPageCodeCompleted;

		public event CheckValidNexonIDnPasswordCompletedEventHandler CheckValidNexonIDnPasswordCompleted;

		public event GetUserIdentitySN_EventCompletedEventHandler GetUserIdentitySN_EventCompleted;

		[SoapDocumentMethod("http://tempuri.org/GetUserInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserInfo(int n4ServiceCode, int n4NexonSN, out string strNexonID, out string strNexonName, out string strRealBirth, out byte n1RealBirthCode, out string strName, out byte n1SexCode, out string strAreaCode, out string strAddress1, out string strAddress2, out string strEmail, out string strPhone, out string strMobilePhone, out byte n1MobileCompanyCode, out byte n1MainPageCode, out byte n1OpenConfigure_Birth, out byte n1OpenConfigure_Name, out byte n1OpenConfigure_Sex, out byte n1OpenConfigure_Area, out byte n1OpenConfigure_Email, out byte n1OpenConfigure_Phone, out byte n1OpenConfigure_School, out DataSet dsSchoolList, out int n4SchoolTotalCount)
		{
			object[] array = base.Invoke("GetUserInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN
			});
			strNexonID = (string)array[1];
			strNexonName = (string)array[2];
			strRealBirth = (string)array[3];
			n1RealBirthCode = (byte)array[4];
			strName = (string)array[5];
			n1SexCode = (byte)array[6];
			strAreaCode = (string)array[7];
			strAddress1 = (string)array[8];
			strAddress2 = (string)array[9];
			strEmail = (string)array[10];
			strPhone = (string)array[11];
			strMobilePhone = (string)array[12];
			n1MobileCompanyCode = (byte)array[13];
			n1MainPageCode = (byte)array[14];
			n1OpenConfigure_Birth = (byte)array[15];
			n1OpenConfigure_Name = (byte)array[16];
			n1OpenConfigure_Sex = (byte)array[17];
			n1OpenConfigure_Area = (byte)array[18];
			n1OpenConfigure_Email = (byte)array[19];
			n1OpenConfigure_Phone = (byte)array[20];
			n1OpenConfigure_School = (byte)array[21];
			dsSchoolList = (DataSet)array[22];
			n4SchoolTotalCount = (int)array[23];
			return (int)array[0];
		}

		public void GetUserInfoAsync(int n4ServiceCode, int n4NexonSN)
		{
			this.GetUserInfoAsync(n4ServiceCode, n4NexonSN, null);
		}

		public void GetUserInfoAsync(int n4ServiceCode, int n4NexonSN, object userState)
		{
			if (this.GetUserInfoOperationCompleted == null)
			{
				this.GetUserInfoOperationCompleted = new SendOrPostCallback(this.OnGetUserInfoOperationCompleted);
			}
			base.InvokeAsync("GetUserInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN
			}, this.GetUserInfoOperationCompleted, userState);
		}

		private void OnGetUserInfoOperationCompleted(object arg)
		{
			if (this.GetUserInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserInfoCompleted(this, new GetUserInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetUserWriteStatusCode", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserWriteStatusCode(int n4ServiceCode, int n4NexonSN, out byte n1WriteStatusCode)
		{
			object[] array = base.Invoke("GetUserWriteStatusCode", new object[]
			{
				n4ServiceCode,
				n4NexonSN
			});
			n1WriteStatusCode = (byte)array[1];
			return (int)array[0];
		}

		public void GetUserWriteStatusCodeAsync(int n4ServiceCode, int n4NexonSN)
		{
			this.GetUserWriteStatusCodeAsync(n4ServiceCode, n4NexonSN, null);
		}

		public void GetUserWriteStatusCodeAsync(int n4ServiceCode, int n4NexonSN, object userState)
		{
			if (this.GetUserWriteStatusCodeOperationCompleted == null)
			{
				this.GetUserWriteStatusCodeOperationCompleted = new SendOrPostCallback(this.OnGetUserWriteStatusCodeOperationCompleted);
			}
			base.InvokeAsync("GetUserWriteStatusCode", new object[]
			{
				n4ServiceCode,
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

		[SoapDocumentMethod("http://tempuri.org/GetUserNexonSN_ByNexonName", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserNexonSN_ByNexonName(int n4ServiceCode, string strNexonName, out int n4NexonSN)
		{
			object[] array = base.Invoke("GetUserNexonSN_ByNexonName", new object[]
			{
				n4ServiceCode,
				strNexonName
			});
			n4NexonSN = (int)array[1];
			return (int)array[0];
		}

		public void GetUserNexonSN_ByNexonNameAsync(int n4ServiceCode, string strNexonName)
		{
			this.GetUserNexonSN_ByNexonNameAsync(n4ServiceCode, strNexonName, null);
		}

		public void GetUserNexonSN_ByNexonNameAsync(int n4ServiceCode, string strNexonName, object userState)
		{
			if (this.GetUserNexonSN_ByNexonNameOperationCompleted == null)
			{
				this.GetUserNexonSN_ByNexonNameOperationCompleted = new SendOrPostCallback(this.OnGetUserNexonSN_ByNexonNameOperationCompleted);
			}
			base.InvokeAsync("GetUserNexonSN_ByNexonName", new object[]
			{
				n4ServiceCode,
				strNexonName
			}, this.GetUserNexonSN_ByNexonNameOperationCompleted, userState);
		}

		private void OnGetUserNexonSN_ByNexonNameOperationCompleted(object arg)
		{
			if (this.GetUserNexonSN_ByNexonNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserNexonSN_ByNexonNameCompleted(this, new GetUserNexonSN_ByNexonNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetUserNexonSN_ByNexonID", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserNexonSN_ByNexonID(int n4ServiceCode, string strNexonID, out int n4NexonSN)
		{
			object[] array = base.Invoke("GetUserNexonSN_ByNexonID", new object[]
			{
				n4ServiceCode,
				strNexonID
			});
			n4NexonSN = (int)array[1];
			return (int)array[0];
		}

		public void GetUserNexonSN_ByNexonIDAsync(int n4ServiceCode, string strNexonID)
		{
			this.GetUserNexonSN_ByNexonIDAsync(n4ServiceCode, strNexonID, null);
		}

		public void GetUserNexonSN_ByNexonIDAsync(int n4ServiceCode, string strNexonID, object userState)
		{
			if (this.GetUserNexonSN_ByNexonIDOperationCompleted == null)
			{
				this.GetUserNexonSN_ByNexonIDOperationCompleted = new SendOrPostCallback(this.OnGetUserNexonSN_ByNexonIDOperationCompleted);
			}
			base.InvokeAsync("GetUserNexonSN_ByNexonID", new object[]
			{
				n4ServiceCode,
				strNexonID
			}, this.GetUserNexonSN_ByNexonIDOperationCompleted, userState);
		}

		private void OnGetUserNexonSN_ByNexonIDOperationCompleted(object arg)
		{
			if (this.GetUserNexonSN_ByNexonIDCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserNexonSN_ByNexonIDCompleted(this, new GetUserNexonSN_ByNexonIDCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/CheckNexonName", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int CheckNexonName(int n4ServiceCode, int n4NexonSN, string strNexonName, bool isCheckChangeCount)
		{
			object[] array = base.Invoke("CheckNexonName", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonName,
				isCheckChangeCount
			});
			return (int)array[0];
		}

		public void CheckNexonNameAsync(int n4ServiceCode, int n4NexonSN, string strNexonName, bool isCheckChangeCount)
		{
			this.CheckNexonNameAsync(n4ServiceCode, n4NexonSN, strNexonName, isCheckChangeCount, null);
		}

		public void CheckNexonNameAsync(int n4ServiceCode, int n4NexonSN, string strNexonName, bool isCheckChangeCount, object userState)
		{
			if (this.CheckNexonNameOperationCompleted == null)
			{
				this.CheckNexonNameOperationCompleted = new SendOrPostCallback(this.OnCheckNexonNameOperationCompleted);
			}
			base.InvokeAsync("CheckNexonName", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonName,
				isCheckChangeCount
			}, this.CheckNexonNameOperationCompleted, userState);
		}

		private void OnCheckNexonNameOperationCompleted(object arg)
		{
			if (this.CheckNexonNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckNexonNameCompleted(this, new CheckNexonNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/ModifyNexonName", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int ModifyNexonName(int n4ServiceCode, int n4NexonSN, string strNexonName, bool isCheckChangeCount, byte n1NexonNameChangeUse)
		{
			object[] array = base.Invoke("ModifyNexonName", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonName,
				isCheckChangeCount,
				n1NexonNameChangeUse
			});
			return (int)array[0];
		}

		public void ModifyNexonNameAsync(int n4ServiceCode, int n4NexonSN, string strNexonName, bool isCheckChangeCount, byte n1NexonNameChangeUse)
		{
			this.ModifyNexonNameAsync(n4ServiceCode, n4NexonSN, strNexonName, isCheckChangeCount, n1NexonNameChangeUse, null);
		}

		public void ModifyNexonNameAsync(int n4ServiceCode, int n4NexonSN, string strNexonName, bool isCheckChangeCount, byte n1NexonNameChangeUse, object userState)
		{
			if (this.ModifyNexonNameOperationCompleted == null)
			{
				this.ModifyNexonNameOperationCompleted = new SendOrPostCallback(this.OnModifyNexonNameOperationCompleted);
			}
			base.InvokeAsync("ModifyNexonName", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonName,
				isCheckChangeCount,
				n1NexonNameChangeUse
			}, this.ModifyNexonNameOperationCompleted, userState);
		}

		private void OnModifyNexonNameOperationCompleted(object arg)
		{
			if (this.ModifyNexonNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ModifyNexonNameCompleted(this, new ModifyNexonNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/ModifyOpenConfigure", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int ModifyOpenConfigure(int n4ServiceCode, int n4NexonSN, byte n1OpenConfigure_Birth, byte n1OpenConfigure_Name, byte n1OpenConfigure_Sex, byte n1OpenConfigure_Area, byte n1OpenConfigure_Email, byte n1OpenConfigure_Phone, byte n1OpenConfigure_School)
		{
			object[] array = base.Invoke("ModifyOpenConfigure", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				n1OpenConfigure_Birth,
				n1OpenConfigure_Name,
				n1OpenConfigure_Sex,
				n1OpenConfigure_Area,
				n1OpenConfigure_Email,
				n1OpenConfigure_Phone,
				n1OpenConfigure_School
			});
			return (int)array[0];
		}

		public void ModifyOpenConfigureAsync(int n4ServiceCode, int n4NexonSN, byte n1OpenConfigure_Birth, byte n1OpenConfigure_Name, byte n1OpenConfigure_Sex, byte n1OpenConfigure_Area, byte n1OpenConfigure_Email, byte n1OpenConfigure_Phone, byte n1OpenConfigure_School)
		{
			this.ModifyOpenConfigureAsync(n4ServiceCode, n4NexonSN, n1OpenConfigure_Birth, n1OpenConfigure_Name, n1OpenConfigure_Sex, n1OpenConfigure_Area, n1OpenConfigure_Email, n1OpenConfigure_Phone, n1OpenConfigure_School, null);
		}

		public void ModifyOpenConfigureAsync(int n4ServiceCode, int n4NexonSN, byte n1OpenConfigure_Birth, byte n1OpenConfigure_Name, byte n1OpenConfigure_Sex, byte n1OpenConfigure_Area, byte n1OpenConfigure_Email, byte n1OpenConfigure_Phone, byte n1OpenConfigure_School, object userState)
		{
			if (this.ModifyOpenConfigureOperationCompleted == null)
			{
				this.ModifyOpenConfigureOperationCompleted = new SendOrPostCallback(this.OnModifyOpenConfigureOperationCompleted);
			}
			base.InvokeAsync("ModifyOpenConfigure", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				n1OpenConfigure_Birth,
				n1OpenConfigure_Name,
				n1OpenConfigure_Sex,
				n1OpenConfigure_Area,
				n1OpenConfigure_Email,
				n1OpenConfigure_Phone,
				n1OpenConfigure_School
			}, this.ModifyOpenConfigureOperationCompleted, userState);
		}

		private void OnModifyOpenConfigureOperationCompleted(object arg)
		{
			if (this.ModifyOpenConfigureCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ModifyOpenConfigureCompleted(this, new ModifyOpenConfigureCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/ModifyRealBirth", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int ModifyRealBirth(int n4ServiceCode, int n4NexonSN, string strRealBirth_Year, string strRealBirth_Month, string strRealBirth_Day, byte n1RealBirthCode)
		{
			object[] array = base.Invoke("ModifyRealBirth", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strRealBirth_Year,
				strRealBirth_Month,
				strRealBirth_Day,
				n1RealBirthCode
			});
			return (int)array[0];
		}

		public void ModifyRealBirthAsync(int n4ServiceCode, int n4NexonSN, string strRealBirth_Year, string strRealBirth_Month, string strRealBirth_Day, byte n1RealBirthCode)
		{
			this.ModifyRealBirthAsync(n4ServiceCode, n4NexonSN, strRealBirth_Year, strRealBirth_Month, strRealBirth_Day, n1RealBirthCode, null);
		}

		public void ModifyRealBirthAsync(int n4ServiceCode, int n4NexonSN, string strRealBirth_Year, string strRealBirth_Month, string strRealBirth_Day, byte n1RealBirthCode, object userState)
		{
			if (this.ModifyRealBirthOperationCompleted == null)
			{
				this.ModifyRealBirthOperationCompleted = new SendOrPostCallback(this.OnModifyRealBirthOperationCompleted);
			}
			base.InvokeAsync("ModifyRealBirth", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strRealBirth_Year,
				strRealBirth_Month,
				strRealBirth_Day,
				n1RealBirthCode
			}, this.ModifyRealBirthOperationCompleted, userState);
		}

		private void OnModifyRealBirthOperationCompleted(object arg)
		{
			if (this.ModifyRealBirthCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ModifyRealBirthCompleted(this, new ModifyRealBirthCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/ModifySchoolInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int ModifySchoolInfo(int n4ServiceCode, int n4NexonSN, int n4SchoolSN, [XmlArrayItem("ArrayOfInt"), XmlArrayItem(IsNullable = false, NestingLevel = 1)] int[][] arrSchoolList)
		{
			object[] array = base.Invoke("ModifySchoolInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				n4SchoolSN,
				arrSchoolList
			});
			return (int)array[0];
		}

		public void ModifySchoolInfoAsync(int n4ServiceCode, int n4NexonSN, int n4SchoolSN, int[][] arrSchoolList)
		{
			this.ModifySchoolInfoAsync(n4ServiceCode, n4NexonSN, n4SchoolSN, arrSchoolList, null);
		}

		public void ModifySchoolInfoAsync(int n4ServiceCode, int n4NexonSN, int n4SchoolSN, int[][] arrSchoolList, object userState)
		{
			if (this.ModifySchoolInfoOperationCompleted == null)
			{
				this.ModifySchoolInfoOperationCompleted = new SendOrPostCallback(this.OnModifySchoolInfoOperationCompleted);
			}
			base.InvokeAsync("ModifySchoolInfo", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				n4SchoolSN,
				arrSchoolList
			}, this.ModifySchoolInfoOperationCompleted, userState);
		}

		private void OnModifySchoolInfoOperationCompleted(object arg)
		{
			if (this.ModifySchoolInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ModifySchoolInfoCompleted(this, new ModifySchoolInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/ModifyMainPageCode", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int ModifyMainPageCode(int n4ServiceCode, int n4NexonSN, byte n1MainPageCode)
		{
			object[] array = base.Invoke("ModifyMainPageCode", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				n1MainPageCode
			});
			return (int)array[0];
		}

		public void ModifyMainPageCodeAsync(int n4ServiceCode, int n4NexonSN, byte n1MainPageCode)
		{
			this.ModifyMainPageCodeAsync(n4ServiceCode, n4NexonSN, n1MainPageCode, null);
		}

		public void ModifyMainPageCodeAsync(int n4ServiceCode, int n4NexonSN, byte n1MainPageCode, object userState)
		{
			if (this.ModifyMainPageCodeOperationCompleted == null)
			{
				this.ModifyMainPageCodeOperationCompleted = new SendOrPostCallback(this.OnModifyMainPageCodeOperationCompleted);
			}
			base.InvokeAsync("ModifyMainPageCode", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				n1MainPageCode
			}, this.ModifyMainPageCodeOperationCompleted, userState);
		}

		private void OnModifyMainPageCodeOperationCompleted(object arg)
		{
			if (this.ModifyMainPageCodeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ModifyMainPageCodeCompleted(this, new ModifyMainPageCodeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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

		[SoapDocumentMethod("http://tempuri.org/GetUserIdentitySN_Event", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetUserIdentitySN_Event(int n4ServiceCode, int n4NexonSN, string strNexonName_Recommended, out long n8IdentitySN, out long n8IdentitySN_Recommended, out int n4NexonSN_Recommended)
		{
			object[] array = base.Invoke("GetUserIdentitySN_Event", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonName_Recommended
			});
			n8IdentitySN = (long)array[1];
			n8IdentitySN_Recommended = (long)array[2];
			n4NexonSN_Recommended = (int)array[3];
			return (int)array[0];
		}

		public void GetUserIdentitySN_EventAsync(int n4ServiceCode, int n4NexonSN, string strNexonName_Recommended)
		{
			this.GetUserIdentitySN_EventAsync(n4ServiceCode, n4NexonSN, strNexonName_Recommended, null);
		}

		public void GetUserIdentitySN_EventAsync(int n4ServiceCode, int n4NexonSN, string strNexonName_Recommended, object userState)
		{
			if (this.GetUserIdentitySN_EventOperationCompleted == null)
			{
				this.GetUserIdentitySN_EventOperationCompleted = new SendOrPostCallback(this.OnGetUserIdentitySN_EventOperationCompleted);
			}
			base.InvokeAsync("GetUserIdentitySN_Event", new object[]
			{
				n4ServiceCode,
				n4NexonSN,
				strNexonName_Recommended
			}, this.GetUserIdentitySN_EventOperationCompleted, userState);
		}

		private void OnGetUserIdentitySN_EventOperationCompleted(object arg)
		{
			if (this.GetUserIdentitySN_EventCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserIdentitySN_EventCompleted(this, new GetUserIdentitySN_EventCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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

		private SendOrPostCallback GetUserInfoOperationCompleted;

		private SendOrPostCallback GetUserWriteStatusCodeOperationCompleted;

		private SendOrPostCallback GetUserNexonSN_ByNexonNameOperationCompleted;

		private SendOrPostCallback GetUserNexonSN_ByNexonIDOperationCompleted;

		private SendOrPostCallback CheckNexonNameOperationCompleted;

		private SendOrPostCallback ModifyNexonNameOperationCompleted;

		private SendOrPostCallback ModifyOpenConfigureOperationCompleted;

		private SendOrPostCallback ModifyRealBirthOperationCompleted;

		private SendOrPostCallback ModifySchoolInfoOperationCompleted;

		private SendOrPostCallback ModifyMainPageCodeOperationCompleted;

		private SendOrPostCallback CheckValidNexonIDnPasswordOperationCompleted;

		private SendOrPostCallback GetUserIdentitySN_EventOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;
	}
}
