using System;

namespace ServiceCore.EndPointNetwork
{
	public enum InvitationRejectReason
	{
		NoSuchCharacter,
		ClientNotLoggedOn,
		AlreadyInvited,
		YouAreNotPartyMaster,
		AlreadyInParty,
		Rejected,
		PartyIsFull,
		GameStarted,
		Captcha,
		Punished,
		NotAvailable,
		PleaseWait,
		SystemError
	}
}
