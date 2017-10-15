using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.MicroPlayServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GameJoinMemberInfo
	{
		public int Order { get; set; }

		public int Tag { get; set; }

		public int Key { get; set; }

		public string Name { get; set; }

		public byte BaseClass { get; set; }

		public int Level { get; set; }

		public int Exp { get; set; }

		public int LevelUpExp { get; set; }

		public int TitleID { get; set; }

		public int TitleCount { get; set; }

		public IDictionary<string, int> Stats { get; set; }

		public BattleInventory BattleInventory { get; set; }

		public CostumeInfo CostumeInfo { get; set; }

		public PetStatusInfo Pet { get; set; }

		public IDictionary<string, int> SkillList { get; set; }

		public IDictionary<int, string> SpSkills { get; set; }

		public IDictionary<string, int> VocationSkills { get; set; }

		public int TransformCoolDown { get; set; }

		public IDictionary<string, BriefSkillEnhance> SkillEnhanceDic { get; set; }

		public IDictionary<int, int> DefMap { get; set; }

		public IDictionary<int, int> ArmorHPMap { get; set; }

		public Dictionary<int, string> EquippedItems { get; set; }

		public IList<int> AbilityList { get; set; }

		public IDictionary<string, int> StatusEffectDict { get; set; }

		public List<StatusEffectElement> StatusEffects { get; set; }

		public int DestroyedDef { get; set; }

		public bool IsTeacher { get; set; }

		public bool IsAssist { get; set; }

		public IDictionary<string, int> GuildLevelBonus { get; set; }

		public bool IsAlive { get; set; }

		public override string ToString()
		{
			return string.Format("GameJoinMemberInfo(name = {0} Order = {1}...)", this.Name, this.Order);
		}
	}
}
