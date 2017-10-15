using System;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class EquippedItemInfo
	{
		public int PartNum { get; set; }

		public bool IsDecoration { get; set; }

		public int CostumePartNum { get; set; }

		public int CostumeSN { get; set; }

		public int Color1 { get; set; }

		public int Color2 { get; set; }

		public int Color3 { get; set; }

		public int ItemLevel { get; set; }

		public double Weight { get; set; }

		public int Durability { get; set; }

		public int MaxDurability { get; set; }

		public int ArmorHP { get; set; }

		public string EquipClass { get; set; }

		public string ItemClass { get; set; }

		public bool IsAvatar { get; set; }

		public CharacterStats StatBonus { get; set; }

		public int DEF_Destroyed { get; set; }

		public int? Effect { get; set; }

		public override string ToString()
		{
			return string.Format("(part = {0}, {1} type = {2}, {3})", new object[]
			{
				this.CostumePartNum,
				this.IsDecoration,
				this.CostumeSN,
				this.EquipClass
			});
		}
	}
}
