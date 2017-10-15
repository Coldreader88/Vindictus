using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LoginFailMessage : IMessage
	{
		public int Reason { get; set; }

		public string BannedReason { get; set; }

		public override string ToString()
		{
			return string.Format("LoginFailMessage[]", new object[0]);
		}

		public enum FailReason
		{
			None,
			PassportFail,
			DuplicateLogin,
			NoUserInfo,
			Banned,
			CafeLoginFail,
			NotGrantedByIP,
			DeniedByIP,
			RelayFail,
			ListCharacterFail,
			SelectCharacterFail,
			HackShieldConnectFail,
			WebShutDown,
			PermissionFailed,
			CafeAuthDeniedForbidden,
			CafeAuthDeniedAddressExpired,
			CafeAuthDeniedPrepaidExpired,
			CafeAuthDeniedMaxConnected,
			MachineIDDenied,
			AgeFailed,
			VersionMismatch,
			NoMoreTicket,
			Shutdowned,
			IPBlocked,
			TokenParsingFail
		}
	}
}
