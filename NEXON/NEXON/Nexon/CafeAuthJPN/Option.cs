using System;

namespace Nexon.CafeAuthJPN
{
	public enum Option : byte
	{
		NoOption,
		AddressNotAllowed,
		AddressMaxConnected,
		AddressExpired,
		AccountNotAllowed,
		AccountMaxConnected,
		AccountExpired,
		NetDirectRecommendation,
		DoubleBooking,
		DoubleCoupon,
		WelcomeAccount,
		WelcomeAddress,
		WelcomeNetDirect,
		WelcomeTrial,
		WelcomeCoupon,
		CouponExpired,
		CouponExhausted,
		WelcomePrepaid,
		PrepaidExpired,
		PrepaidExhausted,
		TrialExpired,
		PrepaidUpdate,
		WelcomeLanInfo,
		AccountNotAvailableTime,
		AccountNotWeekend,
		SinglePlayExhausted,
		WelcomeSinglePlay,
		AccountMachineIDBlocked = 28,
		PossibleCafeCanUseUser = 90,
		PossibleCafeUsedUser,
		PossibleCafeAbsenceUser,
		ImpossibleCafe,
		ImpossibleCafeAbsenceUse,
		ImpossibleCafeUsedUser
	}
}
