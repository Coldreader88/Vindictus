using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class InformCharacterInfo : Operation
	{
		public BaseCharacter? CharacterClass { get; set; }

		public string CharacterName { get; set; }

		public int? Level { get; set; }

		public Dictionary<string, int> SkillRank { get; set; }

		public Dictionary<int, int> Titles { get; set; }

		public int SelectedTitle { get; set; }

		public InformCharacterInfo(BaseCharacter? characterClass, string name, int? level, Dictionary<string, int> skillRank, Dictionary<int, int> titles, int selectedTitle)
		{
			this.CharacterClass = characterClass;
			this.CharacterName = name;
			this.Level = level;
			this.SkillRank = skillRank;
			this.Titles = titles;
			this.SelectedTitle = selectedTitle;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
