using System;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class DetailDEFInfo
	{
		public string EquipClass { get; set; }

		public int ItemLevel { get; set; }

		public int DEF { get; set; }

		public DetailDEFInfo(string EquipClass, int ItemLevel, int DEF)
		{
			this.EquipClass = EquipClass;
			this.ItemLevel = ItemLevel;
			this.DEF = DEF;
		}
	}
}
