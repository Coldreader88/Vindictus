using System;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class ColoredEquipment
	{
		public string ItemClass { get; set; }

		public int Color1 { get; set; }

		public int Color2 { get; set; }

		public int Color3 { get; set; }
	}
}
