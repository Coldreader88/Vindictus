using System;
using System.Net;
using System.Web;
using NPL.SSO.com.nexon.auth;
using NPL.SSO.Configuration;

namespace NPL.SSO
{
	public static class Authenticator
	{
		private static Default CreateClient()
		{
			return new Default
			{
				UseDefaultCredentials = false,
				Timeout = Config.Authenticator.Soap.DefaultTimeout
			};
		}

		private static string GetHostFromPassport(string passport)
		{
			string[] array = passport.Split(new char[]
			{
				':'
			});
			if (array.Length < 4)
			{
				throw new Authenticator.InvalidPassportException();
			}
			return array[1];
		}

		private static Default CreateClientFromID(string id)
		{
			Default @default = Authenticator.CreateClient();
			if (Config.Authenticator.Soap.UseSingleHost)
			{
				@default.Url = @default.Url.Replace("sso.nexon.com", Config.Authenticator.Soap.Host);
				if (Config.Authenticator.Soap.HostProxy.Length > 0)
				{
					@default.Proxy = new WebProxy(Config.Authenticator.Soap.HostProxy);
				}
			}
			else
			{
				byte index = Utility.ComputeHash(id);
				@default.Url = @default.Url.Replace("sso.nexon.com", Config.Authenticator.Servers[(int)index].Host + "." + Config.Authenticator.Soap.Domain);
				if (Config.Authenticator.Soap.UseIP)
				{
					@default.Proxy = new WebProxy(Config.Authenticator.Servers[(int)index].IP);
				}
			}
			return @default;
		}

		private static Default CreateClientFromPassport(string passport)
		{
			Default @default = Authenticator.CreateClient();
			if (Config.Authenticator.Soap.UseSingleHost)
			{
				@default.Url = @default.Url.Replace("sso.nexon.com", Config.Authenticator.Soap.Host);
				if (Config.Authenticator.Soap.HostProxy.Length > 0)
				{
					@default.Proxy = new WebProxy(Config.Authenticator.Soap.HostProxy);
				}
			}
			else
			{
				string hostFromPassport = Authenticator.GetHostFromPassport(passport);
				@default.Url = @default.Url.Replace("sso.nexon.com", hostFromPassport + "." + Config.Authenticator.Soap.Domain);
				if (Config.Authenticator.Soap.UseIP)
				{
					@default.Proxy = new WebProxy(Config.Authenticator.Servers[hostFromPassport].IP);
				}
			}
			return @default;
		}

		private static Default CreateClientFromSN(int sn)
		{
			if (sn <= 0)
			{
				return null;
			}
			Default @default = Authenticator.CreateClient();
			if (Config.Authenticator.Soap.UseSingleHost)
			{
				@default.Url = @default.Url.Replace("sso.nexon.com", Config.Authenticator.Soap.Host);
				if (Config.Authenticator.Soap.HostProxy.Length > 0)
				{
					@default.Proxy = new WebProxy(Config.Authenticator.Soap.HostProxy);
				}
			}
			else
			{
				byte index = Utility.ComputeHash(sn);
				@default.Url = @default.Url.Replace("sso.nexon.com", Config.Authenticator.Servers[(int)index].Host + "." + Config.Authenticator.Soap.Domain);
				if (Config.Authenticator.Soap.UseIP)
				{
					@default.Proxy = new WebProxy(Config.Authenticator.Servers[(int)index].IP);
				}
			}
			return @default;
		}

		public static ErrorCode Login(string id, string pwd, string ip, int gameCode, int locale, out string passport, out string errorMessage)
		{
			uint num;
			uint num2;
			return Authenticator.Login(id, pwd, ip, gameCode, locale, out passport, out num, out num2, out errorMessage);
		}

		public static ErrorCode Login(string id, string pwd, string ip, int gameCode, int locale, out string passport, out string errorMessage, out int subErrCode)
		{
			uint num;
			uint num2;
			return Authenticator.Login(id, pwd, ip, gameCode, locale, 0, out passport, out num, out num2, out errorMessage, out subErrCode);
		}

		public static ErrorCode Login(string id, string pwd, string ip, int gameCode, int locale, int region, out string passport, out string errorMessage, out int subErrCode)
		{
			uint num;
			uint num2;
			return Authenticator.Login(id, pwd, ip, gameCode, locale, region, out passport, out num, out num2, out errorMessage, out subErrCode);
		}

		public static ErrorCode Login(string id, string pwd, string ip, out string passport, out string errorMessage)
		{
			uint num;
			uint num2;
			return Authenticator.Login(id, pwd, ip, 0, 0, out passport, out num, out num2, out errorMessage);
		}

		public static ErrorCode Login(string id, string pwd, string ip, out string passport, out string errorMessage, out int subErrCode)
		{
			uint num;
			uint num2;
			return Authenticator.Login(id, pwd, ip, 0, 0, 0, out passport, out num, out num2, out errorMessage, out subErrCode);
		}

		public static ErrorCode Login(string id, string pwd, string ip, out string passport, out uint unreadNoteCount, out uint statusFlag, out string errorMessage)
		{
			return Authenticator.Login(id, pwd, ip, 0, 0, out passport, out unreadNoteCount, out statusFlag, out errorMessage);
		}

		public static ErrorCode Login(string id, string pwd, string ip, out string passport, out uint unreadNoteCount, out uint statusFlag, out string errorMessage, out int subErrCode)
		{
			return Authenticator.Login(id, pwd, ip, 0, 0, 0, out passport, out unreadNoteCount, out statusFlag, out errorMessage, out subErrCode);
		}

		public static ErrorCode Login(string id, string pwd, string ip, int gameCode, int locale, out string passport, out uint unreadNoteCount, out uint statusFlag, out string errorMessage)
		{
			passport = string.Empty;
			unreadNoteCount = 0u;
			statusFlag = 0u;
			if (id == null || pwd == null)
			{
				errorMessage = "'id' or 'pwd' are null.";
				return ErrorCode.InvalidArgument;
			}
			if (ip == null)
			{
				try
				{
					ip = HttpContext.Current.Request.UserHostAddress;
				}
				catch (Exception)
				{
					errorMessage = "Getting user ip address from HttpContext failed.";
					return ErrorCode.InvalidArgument;
				}
			}
			Default @default;
			try
			{
				@default = Authenticator.CreateClientFromID(id);
			}
			catch (Exception ex)
			{
				errorMessage = "A unknown exception occured while creating a soap client." + Environment.NewLine + ex.ToString();
				ErrorLogger.WriteLog(ErrorCode.Unknown, errorMessage, ex.StackTrace, id, passport);
				return ErrorCode.Unknown;
			}
			int num = Config.Authenticator.Soap.RetryCount;
			ErrorCode result;
			for (;;)
			{
				try
				{
					LoginResult loginResult = @default.Login(id, pwd, ip, (uint)gameCode, (uint)locale);
					if (loginResult.nErrorCode == 0 || loginResult.nErrorCode == 20019)
					{
						passport = loginResult.strPassport;
						unreadNoteCount = loginResult.uUnreadNoteCount;
						statusFlag = loginResult.uStatusFlag;
					}
					errorMessage = loginResult.strErrorMessage;
					result = (ErrorCode)loginResult.nErrorCode;
					break;
				}
				catch (Exception ex2)
				{
					if (num == 0 || !(ex2 is WebException))
					{
						errorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex2.ToString();
						ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, errorMessage, ex2.StackTrace, id, passport);
						result = ErrorCode.SoapCallFailed;
						break;
					}
				}
				num--;
				if (num == 0)
				{
					@default.Timeout = Config.Authenticator.Soap.LongTimeout;
				}
			}
			return result;
		}

		public static ErrorCode Login(string id, string pwd, string ip, int gameCode, int locale, int region, out string passport, out uint unreadNoteCount, out uint statusFlag, out string errorMessage, out int subErrCode)
		{
			passport = string.Empty;
			unreadNoteCount = 0u;
			statusFlag = 0u;
			subErrCode = 0;
			if (id == null || pwd == null)
			{
				errorMessage = "'id' or 'pwd' are null.";
				return ErrorCode.InvalidArgument;
			}
			if (ip == null)
			{
				try
				{
					ip = HttpContext.Current.Request.UserHostAddress;
				}
				catch (Exception)
				{
					errorMessage = "Getting user ip address from HttpContext failed.";
					return ErrorCode.InvalidArgument;
				}
			}
			Default @default;
			try
			{
				@default = Authenticator.CreateClientFromID(id);
			}
			catch (Exception ex)
			{
				errorMessage = "A unknown exception occured while creating a soap client." + Environment.NewLine + ex.ToString();
				ErrorLogger.WriteLog(ErrorCode.Unknown, errorMessage, ex.StackTrace, id, passport);
				return ErrorCode.Unknown;
			}
			int num = Config.Authenticator.Soap.RetryCount;
			ErrorCode result;
			for (;;)
			{
				try
				{
					LoginResult2 loginResult = @default.Login4(id, pwd, ip, (uint)gameCode, (uint)locale, (uint)region);
					if (loginResult.nErrorCode == 0 || loginResult.nErrorCode == 20019)
					{
						passport = loginResult.strPassport;
						unreadNoteCount = loginResult.uUnreadNoteCount;
						statusFlag = loginResult.uStatusFlag;
					}
					subErrCode = loginResult.nSubErrCode;
					errorMessage = loginResult.strErrorMessage;
					result = (ErrorCode)loginResult.nErrorCode;
					break;
				}
				catch (Exception ex2)
				{
					if (num == 0 || !(ex2 is WebException))
					{
						errorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex2.ToString();
						ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, errorMessage, ex2.StackTrace, id, passport);
						result = ErrorCode.SoapCallFailed;
						break;
					}
				}
				num--;
				if (num == 0)
				{
					@default.Timeout = Config.Authenticator.Soap.LongTimeout;
				}
			}
			return result;
		}

		public static ErrorCode MobileLogin(string loginID, string password, string clientIP, out long nexonSN, out string nexonID, out ushort age, out bool newMembership, out byte mainAuthLevel, out byte subAuthLevel, out string errorMessage)
		{
			nexonSN = 0L;
			nexonID = string.Empty;
			age = 0;
			newMembership = false;
			mainAuthLevel = 0;
			subAuthLevel = 0;
			if (loginID == null || password == null)
			{
				errorMessage = "'id' or 'pwd' are null.";
				return ErrorCode.InvalidArgument;
			}
			if (clientIP == null)
			{
				try
				{
					clientIP = HttpContext.Current.Request.UserHostAddress;
				}
				catch (Exception)
				{
					errorMessage = "Getting user ip address from HttpContext failed.";
					return ErrorCode.InvalidArgument;
				}
			}
			Default @default;
			try
			{
				@default = Authenticator.CreateClientFromID(loginID);
			}
			catch (Exception ex)
			{
				errorMessage = "A unknown exception occured while creating a soap client." + Environment.NewLine + ex.ToString();
				ErrorLogger.WriteLog(ErrorCode.Unknown, errorMessage, ex.StackTrace, loginID, string.Empty);
				return ErrorCode.Unknown;
			}
			for (int i = 0; i <= Config.Authenticator.Soap.RetryCount; i++)
			{
				try
				{
					MobileLoginResult mobileLoginResult = @default.MobileLogin(loginID, password, clientIP);
					nexonSN = mobileLoginResult.nNexonSN;
					nexonID = mobileLoginResult.strNexonID;
					age = mobileLoginResult.uAge;
					newMembership = mobileLoginResult.bNewMembership;
					mainAuthLevel = mobileLoginResult.nMainAuthLevel;
					subAuthLevel = mobileLoginResult.nSubAuthLevel;
					errorMessage = mobileLoginResult.strErrorMessage;
					return (ErrorCode)mobileLoginResult.nErrorCode;
				}
				catch (Exception ex2)
				{
					if (i == Config.Authenticator.Soap.RetryCount || !(ex2 is WebException))
					{
						errorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex2.ToString();
						ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, errorMessage, ex2.StackTrace, loginID, string.Empty);
						return ErrorCode.SoapCallFailed;
					}
				}
				if (i == Config.Authenticator.Soap.RetryCount - 1)
				{
					@default.Timeout = Config.Authenticator.Soap.LongTimeout;
				}
			}
			errorMessage = string.Empty;
			return ErrorCode.Unknown;
		}

		public static ErrorCode Logout(string passport, string ip, out string errorMessage)
		{
			if (passport == null || passport.Length < 10 || !passport.StartsWith("NP"))
			{
				errorMessage = "'passport' is null or corrupted.";
				return ErrorCode.InvalidArgument;
			}
			if (ip == null)
			{
				try
				{
					ip = HttpContext.Current.Request.UserHostAddress;
				}
				catch (Exception)
				{
					errorMessage = "Getting user ip address from HttpContext failed.";
					return ErrorCode.InvalidArgument;
				}
			}
			Default @default;
			try
			{
				@default = Authenticator.CreateClientFromPassport(passport);
			}
			catch (Authenticator.InvalidPassportException)
			{
				errorMessage = "'passport' is unrecognizable.";
				return ErrorCode.InvalidArgument;
			}
			catch (Exception ex)
			{
				errorMessage = "A unknown exception occured while creating a soap client." + Environment.NewLine + ex.ToString();
				ErrorLogger.WriteLog(ErrorCode.Unknown, errorMessage, ex.StackTrace, string.Empty, passport);
				return ErrorCode.Unknown;
			}
			ErrorCode result;
			try
			{
				LogoutResult logoutResult = @default.Logout(passport, ip);
				errorMessage = logoutResult.strErrorMessage;
				result = (ErrorCode)logoutResult.nErrorCode;
			}
			catch (Exception ex2)
			{
				errorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex2.ToString();
				ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, errorMessage, ex2.StackTrace, string.Empty, passport);
				result = ErrorCode.SoapCallFailed;
			}
			return result;
		}

		public static ErrorCode CheckSession(string passport, string ip, out string errorMessage)
		{
			uint num;
			uint num2;
			int num3;
			string text;
			string text2;
			byte b;
			ushort num4;
			uint num5;
			uint num6;
			uint num7;
			uint num8;
			string text3;
			string text4;
			byte b2;
			byte b3;
			string text5;
			return Authenticator.CheckSession(passport, ip, null, 0u, out num, out num2, out num3, out text, out text2, out b, out num4, out num5, out num6, out num7, out num8, out text3, out text4, out b2, out b3, out text5, out errorMessage);
		}

		public static ErrorCode CheckSession(string passport, string ip, out uint unreadNoteCount, out uint statusFlag, out string errorMessage)
		{
			int num;
			string text;
			string text2;
			byte b;
			ushort num2;
			uint num3;
			uint num4;
			uint num5;
			uint num6;
			string text3;
			string text4;
			byte b2;
			byte b3;
			string text5;
			return Authenticator.CheckSession(passport, ip, null, 0u, out unreadNoteCount, out statusFlag, out num, out text, out text2, out b, out num2, out num3, out num4, out num5, out num6, out text3, out text4, out b2, out b3, out text5, out errorMessage);
		}

		public static ErrorCode CheckSession(string passport, string ip, out int nexonSN, out string nexonID, out string userIP, out byte sex, out ushort age, out uint flag0, out string errorMessage)
		{
			uint num;
			uint num2;
			uint num3;
			uint num4;
			uint num5;
			string text;
			string text2;
			byte b;
			byte b2;
			string text3;
			return Authenticator.CheckSession(passport, ip, null, 0u, out num, out num2, out nexonSN, out nexonID, out userIP, out sex, out age, out flag0, out num3, out num4, out num5, out text, out text2, out b, out b2, out text3, out errorMessage);
		}

		public static ErrorCode CheckSession(string passport, string ip, out uint unreadNoteCount, out uint statusFlag, out int nexonSN, out string nexonID, out string userIP, out byte sex, out ushort age, out uint flag0, out string errorMessage)
		{
			uint num;
			uint num2;
			uint num3;
			string text;
			string text2;
			byte b;
			byte b2;
			string text3;
			return Authenticator.CheckSession(passport, ip, null, 0u, out unreadNoteCount, out statusFlag, out nexonSN, out nexonID, out userIP, out sex, out age, out flag0, out num, out num2, out num3, out text, out text2, out b, out b2, out text3, out errorMessage);
		}

		public static ErrorCode CheckSession(string passport, string ip, out uint ssnHash, out string errorMessage)
		{
			uint num;
			uint num2;
			int num3;
			string text;
			string text2;
			byte b;
			ushort num4;
			uint num5;
			uint num6;
			uint num7;
			string text3;
			string text4;
			byte b2;
			byte b3;
			string text5;
			return Authenticator.CheckSession(passport, ip, null, 0u, out num, out num2, out num3, out text, out text2, out b, out num4, out num5, out num6, out ssnHash, out num7, out text3, out text4, out b2, out b3, out text5, out errorMessage);
		}

		public static ErrorCode CheckSession(string passport, string ip, out uint unreadNoteCount, out uint statusFlag, out int nexonSN, out string nexonID, out string userIP, out byte sex, out ushort age, out uint flag0, out uint ssnHash, out string errorMessage)
		{
			uint num;
			uint num2;
			string text;
			string text2;
			byte b;
			byte b2;
			string text3;
			return Authenticator.CheckSession(passport, ip, null, 0u, out unreadNoteCount, out statusFlag, out nexonSN, out nexonID, out userIP, out sex, out age, out flag0, out num, out ssnHash, out num2, out text, out text2, out b, out b2, out text3, out errorMessage);
		}

		public static ErrorCode CheckSession(string passport, string ip, string hwid, uint gamecode, out uint unreadNoteCount, out uint statusFlag, out int nexonSN, out string nexonID, out string userIP, out byte sex, out ushort age, out uint flag0, out uint flag1, out uint ssnHash, out uint pwdHash, out string nationCode, out string metaData, out byte secureCode, out byte registeredPC, out string errorMessage)
		{
			registeredPC = 0;
			byte b;
			string text;
			return Authenticator.CheckSession(passport, ip, hwid, gamecode, out unreadNoteCount, out statusFlag, out nexonSN, out nexonID, out userIP, out sex, out age, out flag0, out flag1, out ssnHash, out pwdHash, out nationCode, out metaData, out secureCode, out b, out text, out errorMessage);
		}

		public static ErrorCode CheckSession(string passport, string ip, string hwid, uint gamecode, out uint unreadNoteCount, out uint statusFlag, out int nexonSN, out string nexonID, out string userIP, out byte sex, out ushort age, out uint flag0, out uint flag1, out uint ssnHash, out uint pwdHash, out string nationCode, out string metaData, out byte secureCode, out byte channelCode, out string channelUID, out string errorMessage)
		{
			unreadNoteCount = 0u;
			statusFlag = 0u;
			nexonSN = 0;
			nexonID = string.Empty;
			userIP = string.Empty;
			sex = 0;
			age = 0;
			flag0 = 0u;
			flag1 = 0u;
			ssnHash = 0u;
			pwdHash = 0u;
			nationCode = string.Empty;
			metaData = string.Empty;
			secureCode = 0;
			channelCode = 0;
			channelUID = string.Empty;
			if (passport == null || passport.Length < 10 || !passport.StartsWith("NP"))
			{
				errorMessage = "'passport' is null or corrupted.";
				return ErrorCode.InvalidArgument;
			}
			if (ip == null)
			{
				try
				{
					ip = HttpContext.Current.Request.UserHostAddress;
				}
				catch (Exception)
				{
					errorMessage = "Getting user ip address from HttpContext failed.";
					return ErrorCode.InvalidArgument;
				}
			}
			Default @default;
			try
			{
				@default = Authenticator.CreateClientFromPassport(passport);
			}
			catch (Authenticator.InvalidPassportException)
			{
				errorMessage = "'passport' is unrecognizable.";
				return ErrorCode.InvalidArgument;
			}
			catch (Exception ex)
			{
				errorMessage = "A unknown exception occured while creating a soap client." + Environment.NewLine + ex.ToString();
				ErrorLogger.WriteLog(ErrorCode.Unknown, errorMessage, ex.StackTrace, nexonID, passport);
				return ErrorCode.Unknown;
			}
			int num = Config.Authenticator.Soap.RetryCount;
			ErrorCode result;
			for (;;)
			{
				try
				{
					if (Config.Authenticator.Soap.SoapVersion >= 3)
					{
						CheckSessionAndGetInfo3Result checkSessionAndGetInfo3Result = @default.CheckSessionAndGetInfo3(passport, ip, hwid, gamecode);
						if (checkSessionAndGetInfo3Result.nErrorCode == 0)
						{
							unreadNoteCount = checkSessionAndGetInfo3Result.uUnreadNoteCount;
							statusFlag = checkSessionAndGetInfo3Result.uStatusFlag;
						}
						nexonSN = checkSessionAndGetInfo3Result.nNexonSN;
						nexonID = checkSessionAndGetInfo3Result.strNexonID;
						userIP = Utility.IPToString(checkSessionAndGetInfo3Result.uIP);
						sex = checkSessionAndGetInfo3Result.uSex;
						age = checkSessionAndGetInfo3Result.uAge;
						flag0 = checkSessionAndGetInfo3Result.uFlag0;
						flag1 = checkSessionAndGetInfo3Result.uFlag1;
						ssnHash = checkSessionAndGetInfo3Result.uSsnHash;
						pwdHash = checkSessionAndGetInfo3Result.uPwdHash;
						nationCode = checkSessionAndGetInfo3Result.strNationCode;
						metaData = checkSessionAndGetInfo3Result.strMetaData;
						secureCode = checkSessionAndGetInfo3Result.uSecureCode;
						channelCode = checkSessionAndGetInfo3Result.uChannelCode;
						channelUID = checkSessionAndGetInfo3Result.strChannelUID;
						errorMessage = checkSessionAndGetInfo3Result.strErrorMessage;
						result = (ErrorCode)checkSessionAndGetInfo3Result.nErrorCode;
						break;
					}
					CheckSessionAndGetInfo2Result checkSessionAndGetInfo2Result = @default.CheckSessionAndGetInfo2(passport, ip, hwid, gamecode);
					if (checkSessionAndGetInfo2Result.nErrorCode == 0)
					{
						unreadNoteCount = checkSessionAndGetInfo2Result.uUnreadNoteCount;
						statusFlag = checkSessionAndGetInfo2Result.uStatusFlag;
					}
					nexonSN = checkSessionAndGetInfo2Result.nNexonSN;
					nexonID = checkSessionAndGetInfo2Result.strNexonID;
					userIP = Utility.IPToString(checkSessionAndGetInfo2Result.uIP);
					sex = checkSessionAndGetInfo2Result.uSex;
					age = checkSessionAndGetInfo2Result.uAge;
					flag0 = checkSessionAndGetInfo2Result.uFlag0;
					flag1 = checkSessionAndGetInfo2Result.uFlag1;
					ssnHash = checkSessionAndGetInfo2Result.uSsnHash;
					pwdHash = checkSessionAndGetInfo2Result.uPwdHash;
					nationCode = checkSessionAndGetInfo2Result.strNationCode;
					metaData = checkSessionAndGetInfo2Result.strMetaData;
					secureCode = checkSessionAndGetInfo2Result.uSecureCode;
					errorMessage = checkSessionAndGetInfo2Result.strErrorMessage;
					result = (ErrorCode)checkSessionAndGetInfo2Result.nErrorCode;
					break;
				}
				catch (Exception ex2)
				{
					if (num == 0 || !(ex2 is WebException))
					{
						errorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex2.ToString();
						ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, errorMessage, ex2.StackTrace, nexonID, passport);
						result = ErrorCode.SoapCallFailed;
						break;
					}
				}
				num--;
				if (num == 0)
				{
					@default.Timeout = Config.Authenticator.Soap.LongTimeout;
				}
			}
			return result;
		}

		public static ErrorCode CheckSession2(string passport, string ip, string hwid, uint gamecode, out uint unreadNoteCount, out uint statusFlag, out long nexonSN64, out string nexonID, out string userIP, out byte sex, out ushort age, out string nationCode, out string metaData, out byte secureCode, out byte channelCode, out string channelUID, out bool newMembership, out byte mainAuthLevel, out byte subAuthLevel, out string errorMessage)
		{
			unreadNoteCount = 0u;
			statusFlag = 0u;
			nexonSN64 = 0L;
			nexonID = string.Empty;
			userIP = string.Empty;
			sex = 0;
			age = 0;
			nationCode = string.Empty;
			metaData = string.Empty;
			secureCode = 0;
			channelCode = 0;
			channelUID = string.Empty;
			newMembership = false;
			mainAuthLevel = 0;
			subAuthLevel = 0;
			if (passport == null || passport.Length < 10 || !passport.StartsWith("NP"))
			{
				errorMessage = "'passport' is null or corrupted.";
				return ErrorCode.InvalidArgument;
			}
			if (ip == null)
			{
				try
				{
					ip = HttpContext.Current.Request.UserHostAddress;
				}
				catch (Exception)
				{
					errorMessage = "Getting user ip address from HttpContext failed.";
					return ErrorCode.InvalidArgument;
				}
			}
			Default @default;
			try
			{
				@default = Authenticator.CreateClientFromPassport(passport);
			}
			catch (Authenticator.InvalidPassportException)
			{
				errorMessage = "'passport' is unrecognizable.";
				return ErrorCode.InvalidArgument;
			}
			catch (Exception ex)
			{
				errorMessage = "A unknown exception occured while creating a soap client." + Environment.NewLine + ex.ToString();
				ErrorLogger.WriteLog(ErrorCode.Unknown, errorMessage, ex.StackTrace, nexonID, passport);
				return ErrorCode.Unknown;
			}
			int num = Config.Authenticator.Soap.RetryCount;
			ErrorCode result;
			for (;;)
			{
				try
				{
					if (Config.Authenticator.Soap.SoapVersion == 4)
					{
						CheckSessionAndGetInfo4Result checkSessionAndGetInfo4Result = @default.CheckSessionAndGetInfo4(passport, ip, hwid, gamecode);
						if (checkSessionAndGetInfo4Result.nErrorCode == 0)
						{
							unreadNoteCount = checkSessionAndGetInfo4Result.uUnreadNoteCount;
							statusFlag = checkSessionAndGetInfo4Result.uStatusFlag;
						}
						nexonSN64 = checkSessionAndGetInfo4Result.nNexonSN64;
						nexonID = checkSessionAndGetInfo4Result.strNexonID;
						userIP = Utility.IPToString(checkSessionAndGetInfo4Result.uIP);
						sex = checkSessionAndGetInfo4Result.uSex;
						age = checkSessionAndGetInfo4Result.uAge;
						nationCode = checkSessionAndGetInfo4Result.strNationCode;
						metaData = checkSessionAndGetInfo4Result.strMetaData;
						secureCode = checkSessionAndGetInfo4Result.uSecureCode;
						channelCode = checkSessionAndGetInfo4Result.uChannelCode;
						channelUID = checkSessionAndGetInfo4Result.strChannelUID;
						newMembership = checkSessionAndGetInfo4Result.bNewMembership;
						mainAuthLevel = checkSessionAndGetInfo4Result.nMainAuthLevel;
						subAuthLevel = checkSessionAndGetInfo4Result.nSubAuthLevel;
						errorMessage = checkSessionAndGetInfo4Result.strErrorMessage;
						result = (ErrorCode)checkSessionAndGetInfo4Result.nErrorCode;
						break;
					}
					CheckSessionAndGetInfo2Result checkSessionAndGetInfo2Result = @default.CheckSessionAndGetInfo2(passport, ip, hwid, gamecode);
					if (checkSessionAndGetInfo2Result.nErrorCode == 0)
					{
						unreadNoteCount = checkSessionAndGetInfo2Result.uUnreadNoteCount;
						statusFlag = checkSessionAndGetInfo2Result.uStatusFlag;
					}
					nexonSN64 = (long)checkSessionAndGetInfo2Result.nNexonSN;
					nexonID = checkSessionAndGetInfo2Result.strNexonID;
					userIP = Utility.IPToString(checkSessionAndGetInfo2Result.uIP);
					sex = checkSessionAndGetInfo2Result.uSex;
					age = checkSessionAndGetInfo2Result.uAge;
					nationCode = checkSessionAndGetInfo2Result.strNationCode;
					metaData = checkSessionAndGetInfo2Result.strMetaData;
					secureCode = checkSessionAndGetInfo2Result.uSecureCode;
					newMembership = false;
					mainAuthLevel = 0;
					subAuthLevel = 0;
					errorMessage = checkSessionAndGetInfo2Result.strErrorMessage;
					result = (ErrorCode)checkSessionAndGetInfo2Result.nErrorCode;
					break;
				}
				catch (Exception ex2)
				{
					if (num == 0 || !(ex2 is WebException))
					{
						errorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex2.ToString();
						ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, errorMessage, ex2.StackTrace, nexonID, passport);
						result = ErrorCode.SoapCallFailed;
						break;
					}
				}
				num--;
				if (num == 0)
				{
					@default.Timeout = Config.Authenticator.Soap.LongTimeout;
				}
			}
			return result;
		}

		public static UpgradeGameTokenResult UpgradeGameToken(SecureLoginType type, uint gamecode, string passport, string ip)
		{
			UpgradeGameTokenResult upgradeGameTokenResult = new UpgradeGameTokenResult();
			Default @default = Authenticator.CreateClientFromPassport(passport);
			int num = 0;
			if (@default == null)
			{
				upgradeGameTokenResult.nErrorCode = 20024;
				upgradeGameTokenResult.strErrorMessage = "invalide passport." + Environment.NewLine + "passport : " + passport;
				return upgradeGameTokenResult;
			}
			for (;;)
			{
				try
				{
					UpgradeGameTokenResult upgradeGameTokenResult2 = @default.UpgradeGameToken((byte)type, string.Empty, gamecode, passport, string.Empty, ip);
					upgradeGameTokenResult.nErrorCode = upgradeGameTokenResult2.nErrorCode;
					upgradeGameTokenResult.strErrorMessage = upgradeGameTokenResult2.strErrorMessage;
					upgradeGameTokenResult.strToken = upgradeGameTokenResult2.strToken;
				}
				catch (Exception ex)
				{
					if (num < Config.Authenticator.Soap.RetryCount)
					{
						num++;
						if (num == Config.Authenticator.Soap.RetryCount)
						{
							@default.Timeout = Config.Authenticator.Soap.LongTimeout;
						}
						continue;
					}
					upgradeGameTokenResult.nErrorCode = 20023;
					upgradeGameTokenResult.strErrorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex.ToString();
					ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, upgradeGameTokenResult.strErrorMessage, ex.StackTrace, string.Empty, "type: " + type.ToString() + ", passport: " + passport);
				}
				finally
				{
					@default.Dispose();
				}
				break;
			}
			return upgradeGameTokenResult;
		}

		public static UpdateInfoResult UpdateInfo(int n4NexonSN, UpdateInfoType type, int n4Arg)
		{
			UpdateInfoResult updateInfoResult = new UpdateInfoResult();
			Default @default = Authenticator.CreateClientFromSN(n4NexonSN);
			int num = 0;
			if (@default == null)
			{
				updateInfoResult.nErrorCode = 20024;
				updateInfoResult.strErrorMessage = "invalide nexon sn." + Environment.NewLine + "nNexonSN : " + n4NexonSN.ToString();
				return updateInfoResult;
			}
			for (;;)
			{
				try
				{
					UpdateInfoResult updateInfoResult2 = @default.UpdateInfo(n4NexonSN, (int)type, n4Arg);
					updateInfoResult.nErrorCode = updateInfoResult2.nErrorCode;
					updateInfoResult.strErrorMessage = updateInfoResult2.strErrorMessage;
				}
				catch (Exception ex)
				{
					if (num < Config.Authenticator.Soap.RetryCount)
					{
						num++;
						if (num == Config.Authenticator.Soap.RetryCount)
						{
							@default.Timeout = Config.Authenticator.Soap.LongTimeout;
						}
						continue;
					}
					updateInfoResult.nErrorCode = 20023;
					updateInfoResult.strErrorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex.ToString();
					ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, updateInfoResult.strErrorMessage, ex.StackTrace, string.Empty, "NexonSN: " + n4NexonSN.ToString() + ", Argument: " + n4Arg.ToString());
				}
				finally
				{
					@default.Dispose();
				}
				break;
			}
			return updateInfoResult;
		}

		public static BanResult Ban(int n4NexonSN)
		{
			BanResult banResult = new BanResult();
			Default @default = Authenticator.CreateClientFromSN(n4NexonSN);
			int num = 0;
			if (@default == null)
			{
				banResult.nErrorCode = 20024;
				banResult.strErrorMessage = "invalide nexon sn." + Environment.NewLine + "nNexonSN : " + n4NexonSN.ToString();
				return banResult;
			}
			for (;;)
			{
				try
				{
					BanResult banResult2 = @default.Ban((long)n4NexonSN);
					banResult.nErrorCode = banResult2.nErrorCode;
					banResult.strErrorMessage = banResult2.strErrorMessage;
				}
				catch (Exception ex)
				{
					if (num < Config.Authenticator.Soap.RetryCount)
					{
						num++;
						if (num == Config.Authenticator.Soap.RetryCount)
						{
							@default.Timeout = Config.Authenticator.Soap.LongTimeout;
						}
						continue;
					}
					banResult.nErrorCode = 20023;
					banResult.strErrorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex.ToString();
					ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, banResult.strErrorMessage, ex.StackTrace, string.Empty, "NexonSN: " + n4NexonSN.ToString());
				}
				finally
				{
					@default.Dispose();
				}
				break;
			}
			return banResult;
		}

		public static IsOTPUsableResult IsOTPUsable(int n4NexonSN)
		{
			IsOTPUsableResult isOTPUsableResult = new IsOTPUsableResult();
			Default @default = Authenticator.CreateClientFromSN(n4NexonSN);
			int num = 0;
			if (@default == null)
			{
				isOTPUsableResult.nErrorCode = 20024;
				isOTPUsableResult.strErrorMessage = "invalide nexon sn." + Environment.NewLine + "nNexonSN : " + n4NexonSN.ToString();
				return isOTPUsableResult;
			}
			for (;;)
			{
				try
				{
					IsOTPUsableResult isOTPUsableResult2 = @default.IsOTPUsable(n4NexonSN);
					isOTPUsableResult.nErrorCode = isOTPUsableResult2.nErrorCode;
					isOTPUsableResult.strErrorMessage = isOTPUsableResult2.strErrorMessage;
				}
				catch (Exception ex)
				{
					if (num < Config.Authenticator.Soap.RetryCount)
					{
						num++;
						if (num == Config.Authenticator.Soap.RetryCount)
						{
							@default.Timeout = Config.Authenticator.Soap.LongTimeout;
						}
						continue;
					}
					isOTPUsableResult.nErrorCode = 20023;
					isOTPUsableResult.strErrorMessage = "A unknown exception occured while calling a soap method." + Environment.NewLine + ex.ToString();
					ErrorLogger.WriteLog(ErrorCode.SoapCallFailed, isOTPUsableResult.strErrorMessage, ex.StackTrace, string.Empty, "NexonSN: " + n4NexonSN.ToString());
				}
				finally
				{
					@default.Dispose();
				}
				break;
			}
			return isOTPUsableResult;
		}

		private enum SoapVersion
		{
			CurrentVersion = 4
		}

		private class InvalidPassportException : Exception
		{
		}
	}
}
