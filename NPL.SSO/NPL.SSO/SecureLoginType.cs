using System;

namespace NPL.SSO
{
	public enum SecureLoginType : byte
	{
		Unknown,
		OTP,
		RegisteredPC,
		NexonStick,
		SafeLogin,
		OTP_Season2,
		PhoneAuth,
		NxPlayPass
	}
}
