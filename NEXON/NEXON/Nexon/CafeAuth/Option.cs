using System;

namespace Nexon.CafeAuth
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
		AccountShutdowned,
		AccountMachineIDBlocked,
		AddressNotApplyed = 30,
		AlmostExhausted = 32,
		WelcomeFreePrepaid,
		PossibleCafeCanUseUser = 90,
		PossibleCafeUsedUser,
		PossibleCafeAbsenceUser,
		ImpossibleCafe,
		ImpossibleCafeAbsenceUse,
		ImpossibleCafeUsedUser,
		IPBlockedBySystem = 125
	}
}
