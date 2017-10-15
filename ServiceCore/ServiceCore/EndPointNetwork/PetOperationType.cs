using System;

namespace ServiceCore.EndPointNetwork
{
	public enum PetOperationType
	{
		QueryPetList,
		RemovePet,
		SelectPet,
		LearnSkill,
		_CHEAT_GiveExp,
		_CHEAT_SetLevelEXP,
		_CHEAT_GivePetSkill,
		_CHEAT_RemovePetSkill,
		_CHEAT_GiveSkillPoint,
		_CHEAT_AddPet,
		_CHEAT_RemovePet,
		_CHEAT_GivePetAccessory,
		_CHEAT_SetPetStatus,
		_CHEAT_Refresh,
		_CHEAT_Simulate_EndBattle,
		_CHEAT_FeedPet,
		_CHEAT_ChangePetSlot
	}
}
