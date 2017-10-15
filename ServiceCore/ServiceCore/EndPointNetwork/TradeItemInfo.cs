using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TradeItemInfo : IMessage
	{
		public long TID { get; set; }

		public long CID { get; set; }

		public string ChracterName { get; set; }

		public string ItemClass { get; set; }

		public long ItemCount { get; set; }

		public long ItemPrice { get; set; }

		public int CloseDate { get; set; }

		public bool HasAttribute
		{
			get
			{
				return this.AttributeEX != null;
			}
		}

		public string AttributeEX { get; set; }

		public int MaxArmorCondition { get; set; }

		public int color1 { get; set; }

		public int color2 { get; set; }

		public int color3 { get; set; }

		public int MinPrice { get; set; }

		public int MaxPrice { get; set; }

		public int AvgPrice { get; set; }

		public byte TradeType { get; set; }

		public override string ToString()
		{
			string text = "";
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				"TID ",
				this.TID,
				"\n"
			});
			object obj2 = text;
			text = string.Concat(new object[]
			{
				obj2,
				"CID ",
				this.CID,
				"\n"
			});
			text = text + "CharacterName " + this.ChracterName + "\n";
			text = text + "ItemClass " + this.ItemClass + "\n";
			object obj3 = text;
			text = string.Concat(new object[]
			{
				obj3,
				"ItemCount ",
				this.ItemCount,
				"\n"
			});
			object obj4 = text;
			text = string.Concat(new object[]
			{
				obj4,
				"ItemPrice ",
				this.ItemPrice,
				"\n"
			});
			object obj5 = text;
			text = string.Concat(new object[]
			{
				obj5,
				"CloseDate ",
				this.CloseDate,
				"\n"
			});
			object obj6 = text;
			text = string.Concat(new object[]
			{
				obj6,
				"HasAttribute ",
				this.HasAttribute,
				"\n"
			});
			text = text + "AttributeEX " + this.AttributeEX + "\n";
			object obj7 = text;
			text = string.Concat(new object[]
			{
				obj7,
				"MaxArmorCondition ",
				this.MaxArmorCondition,
				"\n"
			});
			object obj8 = text;
			text = string.Concat(new object[]
			{
				obj8,
				"ArmorCondition ",
				this.MaxArmorCondition,
				"\n"
			});
			object obj9 = text;
			text = string.Concat(new object[]
			{
				obj9,
				"color1 ",
				this.color1,
				"\n"
			});
			object obj10 = text;
			text = string.Concat(new object[]
			{
				obj10,
				"color2 ",
				this.color2,
				"\n"
			});
			object obj11 = text;
			text = string.Concat(new object[]
			{
				obj11,
				"color3 ",
				this.color3,
				"\n"
			});
			object obj12 = text;
			text = string.Concat(new object[]
			{
				obj12,
				"MinPrice ",
				this.MinPrice,
				"\n"
			});
			object obj13 = text;
			text = string.Concat(new object[]
			{
				obj13,
				"MaxPrice ",
				this.MaxPrice,
				"\n"
			});
			object obj14 = text;
			text = string.Concat(new object[]
			{
				obj14,
				"AvgPrice ",
				this.AvgPrice,
				"\n"
			});
			object obj15 = text;
			return string.Concat(new object[]
			{
				obj15,
				"TradeType ",
				this.TradeType,
				"\n"
			});
		}
	}
}
