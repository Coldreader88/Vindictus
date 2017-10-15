using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RouletteSlotInfo
	{
		public string ItemClassEx { get; set; }

		public int ItemCount { get; set; }

		public int Color1 { get; set; }

		public int Color2 { get; set; }

		public int Color3 { get; set; }

		public int Grade { get; set; }

		public RouletteSlotInfo(string itemClassEx, int itemCount, int color1, int color2, int color3, int grade)
		{
			this.ItemClassEx = itemClassEx;
			this.ItemCount = itemCount;
			this.Color1 = color1;
			this.Color2 = color2;
			this.Color3 = color3;
			this.Grade = grade;
		}

		public override string ToString()
		{
			return string.Format("RouletteSlotInfo {0}, {1}\n", this.ItemClassEx, this.Grade);
		}
	}
}
