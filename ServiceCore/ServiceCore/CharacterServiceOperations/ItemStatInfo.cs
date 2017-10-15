using System;
using System.Collections.Generic;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class ItemStatInfo
	{
		public CharacterStats ItemStat { get; set; }

		public ICollection<DetailDEFInfo> DetailDEFInfos { get; set; }

		public IDictionary<string, int> SkillBonus { get; set; }

		public int TotalWeightx100 { get; set; }

		public ItemStatInfo(CharacterStats itemStat, ICollection<DetailDEFInfo> detailDEFInfos, IDictionary<string, int> skillBonus, int totalWeightx100)
		{
			this.ItemStat = this.ItemStat;
			this.DetailDEFInfos = detailDEFInfos;
			this.SkillBonus = skillBonus;
			this.TotalWeightx100 = totalWeightx100;
		}
	}
}
