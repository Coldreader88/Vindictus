using System;
using ServiceCore.EndPointNetwork;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class CharacterSummary
	{
		public long CID { get; set; }

		public int NexonSN { get; set; }

		public string CharacterID { get; set; }

		public int CharacterSN { get; set; }

		public BaseCharacter BaseCharacter { get; set; }

		public int Level { get; set; }

		public int Exp { get; set; }

		public int LevelUpExp { get; set; }

		public int Title { get; set; }

		public int TitleCount { get; set; }

		public CostumeInfo Costume { get; set; }

		public int DailyMicroPlayCount { get; set; }

		public int DailyFreePlayCount { get; set; }

		public string Quote { get; set; }

		public int TotalUsedAP { get; set; }

		public int GuildId { get; set; }

		public string GuildName { get; set; }

		public int CafeType { get; set; }

		public bool IsPremium { get; set; }

		public bool HasBonusEffect { get; set; }

		public bool IsReturn { get; set; }

		public bool IsDeleting { get; set; }

		public int DeleteWaitLeftSec { get; set; }

		public bool IsShouldNameChange { get; set; }

		public int VIPCode { get; set; }

		public VocationEnum VocationClass { get; set; }

		public int VocationLevel { get; set; }

		public int VocationExp { get; set; }

		public int VocationLevelUpExp { get; set; }

		public int VocationSkillPointAvailable { get; set; }

		public int FreeMatchWinCount { get; set; }

		public int FreeMatchLoseCount { get; set; }

		public PetStatusInfo Pet { get; set; }

		public bool IsEventJumping { get; set; }

		public string FreeTitleName { get; set; }

		public int Pattern { get; set; }

		public int ArenaWinCount { get; set; }

		public int ArenaLoseCount { get; set; }

		public int ArenaSuccessiveWinCount { get; set; }

		public CharacterSummary(long cid, int nexonSN, string characterID, int characterSN, BaseCharacter baseCharacter, int level, int exp, int levelUpExp, int title, int titleCount, CostumeInfo costume, int dailyMicroPlayCount, int dailyFreePlayCount, int totalUsedAP, string quote, int gid, string gname, int cafeType, bool isPremium, bool hasBonusEffect, bool isReturn, bool isDeleting, int deleteWaitLeftSec, bool isShouldNameChange, int VIPCode, VocationEnum vocationClass, int vocationLevel, int vocationExp, int vocationLevelUpExp, int vocationSkillPointAvailable, int freeMatchWinCount, int freeMatchLoseCount, PetStatusInfo pet, bool isEventJumping, string FreeTitleName, int pattern, int arenaWinCount, int arenaLoseCount, int arenaSuccessiveWinCount)
		{
			this.CID = cid;
			this.NexonSN = nexonSN;
			this.CharacterID = characterID;
			this.CharacterSN = characterSN;
			this.BaseCharacter = baseCharacter;
			this.Level = level;
			this.Exp = exp;
			this.LevelUpExp = levelUpExp;
			this.Title = title;
			this.TitleCount = titleCount;
			this.Costume = (costume ?? new CostumeInfo());
			this.DailyMicroPlayCount = dailyMicroPlayCount;
			this.DailyFreePlayCount = dailyFreePlayCount;
			this.TotalUsedAP = totalUsedAP;
			this.Quote = (quote ?? "");
			this.GuildId = gid;
			this.GuildName = (gname ?? "");
			this.CafeType = cafeType;
			this.IsPremium = isPremium;
			this.HasBonusEffect = hasBonusEffect;
			this.IsReturn = isReturn;
			this.IsDeleting = isDeleting;
			this.DeleteWaitLeftSec = deleteWaitLeftSec;
			this.IsShouldNameChange = isShouldNameChange;
			this.VIPCode = VIPCode;
			this.VocationClass = vocationClass;
			this.VocationExp = vocationExp;
			this.VocationLevel = vocationLevel;
			this.VocationLevelUpExp = vocationLevelUpExp;
			this.VocationSkillPointAvailable = vocationSkillPointAvailable;
			this.FreeMatchWinCount = freeMatchWinCount;
			this.FreeMatchLoseCount = freeMatchLoseCount;
			this.Pet = pet;
			this.IsEventJumping = isEventJumping;
			this.FreeTitleName = (FreeTitleName ?? "");
			this.Pattern = pattern;
			this.ArenaWinCount = arenaWinCount;
			this.ArenaLoseCount = arenaLoseCount;
			this.ArenaSuccessiveWinCount = arenaSuccessiveWinCount;
		}

		public override string ToString()
		{
			return string.Format("CharacterSummary({0} ({4}) : level {1} {2} {3})", new object[]
			{
				this.CharacterID,
				this.Level,
				this.BaseCharacter,
				this.Costume,
				this.GuildName
			});
		}
	}
}
