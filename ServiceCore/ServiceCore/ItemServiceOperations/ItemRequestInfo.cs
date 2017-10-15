using System;
using System.Collections.Generic;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class ItemRequestInfo
	{
		public List<ItemRequestInfo.Element> Elements { get; private set; }

		public int Count
		{
			get
			{
				return this.Elements.Count;
			}
		}

		public ItemRequestInfo()
		{
			this.Elements = new List<ItemRequestInfo.Element>();
		}

		public ItemRequestInfo(string itemClassEx, int num)
		{
			this.Elements = new List<ItemRequestInfo.Element>
			{
				new ItemRequestInfo.Element(itemClassEx, num, -1, -1, -1, 0, 0, null, false)
			};
		}

		public ItemRequestInfo(IDictionary<string, int> itemDict)
		{
			this.Elements = new List<ItemRequestInfo.Element>();
			foreach (KeyValuePair<string, int> keyValuePair in itemDict)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		public void Add(ItemRequestInfo.Element element)
		{
			this.Elements.Add(element);
		}

		public void Add(string itemClassEx, int num)
		{
			this.Elements.Add(new ItemRequestInfo.Element(itemClassEx, num, -1, -1, -1, 0, 0, null, false));
		}

		public void Add(string itemClassEx, int num, int c1, int c2, int c3, DateTime? expireDate, bool isCharacterBinded)
		{
			this.Elements.Add(new ItemRequestInfo.Element(itemClassEx, num, c1, c2, c3, 0, 0, expireDate, isCharacterBinded));
		}

		[Serializable]
		public class Element
		{
			public string ItemClassEx { get; set; }

			public int Num { get; set; }

			public int Color1 { get; set; }

			public int Color2 { get; set; }

			public int Color3 { get; set; }

			public int ReducedDurability { get; set; }

			public int MaxDurabilityBonus { get; set; }

			public DateTime? ExpireDateTime { get; set; }

			public bool IsCharacterBinded { get; set; }

			public Element(string itemClassEx, int num)
			{
				this.ItemClassEx = itemClassEx;
				this.Num = num;
				this.Color1 = -1;
				this.Color2 = -1;
				this.Color3 = -1;
				this.ReducedDurability = 0;
				this.MaxDurabilityBonus = 0;
				this.ExpireDateTime = null;
				this.IsCharacterBinded = true;
			}

			public Element(ItemRequestInfo.Element element, int num)
			{
				this.ItemClassEx = element.ItemClassEx;
				this.Num = num;
				this.Color1 = element.Color1;
				this.Color2 = element.Color2;
				this.Color3 = element.Color3;
				this.ReducedDurability = element.ReducedDurability;
				this.MaxDurabilityBonus = element.MaxDurabilityBonus;
				this.ExpireDateTime = element.ExpireDateTime;
				this.IsCharacterBinded = element.IsCharacterBinded;
			}

			public Element(string itemClassEx, int num, int color1, int color2, int color3, int reducedDurability, int maxDurabilityBonus, DateTime? expireDateTime, bool isCharacterBinded)
			{
				this.ItemClassEx = itemClassEx;
				this.Num = num;
				this.Color1 = color1;
				this.Color2 = color2;
				this.Color3 = color3;
				this.ReducedDurability = reducedDurability;
				this.MaxDurabilityBonus = maxDurabilityBonus;
				this.ExpireDateTime = expireDateTime;
				this.IsCharacterBinded = isCharacterBinded;
			}

			public override string ToString()
			{
				return string.Format("{0} x {1}", this.ItemClassEx, this.Num);
			}
		}
	}
}
