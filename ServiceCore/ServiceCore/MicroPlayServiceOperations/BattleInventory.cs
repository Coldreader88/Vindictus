using System;
using System.Collections.Generic;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class BattleInventory
	{
		public IDictionary<int, ConsumablesInfo> Consumables { get; private set; }

		public IDictionary<string, ConsumablesInfo> UsedList
		{
			get
			{
				return this.usedlist;
			}
			private set
			{
				this.usedlist = value;
			}
		}

		public BattleInventory()
		{
			this.Consumables = new Dictionary<int, ConsumablesInfo>();
			this.usedlist = new Dictionary<string, ConsumablesInfo>();
		}

		public bool UseConsumable(int part, int slot, out ConsumablesInfo consumable)
		{
			if (!this.Consumables.TryGetValue(part, out consumable))
			{
				return false;
			}
			if (consumable.InnerConsumables.Count == 0)
			{
				return slot == 0 && consumable.Used(1);
			}
			slot--;
			if (slot < 0 || slot >= consumable.InnerConsumables.Count)
			{
				return false;
			}
			consumable = consumable.InnerConsumables[slot];
			return consumable != null && consumable.Used(1);
		}

		public void RemoveConsumable(int part)
		{
			if (this.Consumables.ContainsKey(part))
			{
				if (this.usedlist.ContainsKey(this.Consumables[part].ItemClass))
				{
					this.usedlist[this.Consumables[part].ItemClass] = this.Consumables[part];
				}
				else
				{
					this.usedlist.Add(this.Consumables[part].ItemClass, this.Consumables[part]);
				}
				this.Consumables[part] = new ConsumablesInfo("", 0, 0);
			}
		}

		public void SetConsumable(int part, ConsumablesInfo info)
		{
			this.SetConsumable(part, info, false);
		}

		public void SetConsumable(int part, ConsumablesInfo info, bool ignoreUsedList)
		{
			int consumableKey = this.GetConsumableKey(info);
			if (consumableKey != -1)
			{
				this.RemoveConsumable(consumableKey);
			}
			this.RemoveConsumable(part);
			if (this.usedlist.ContainsKey(info.ItemClass) && !ignoreUsedList)
			{
				this.Consumables[part] = this.usedlist[info.ItemClass];
				return;
			}
			this.Consumables[part] = info;
		}

		public ConsumablesInfo GetConsumable(int part)
		{
			foreach (KeyValuePair<int, ConsumablesInfo> keyValuePair in this.Consumables)
			{
				if (keyValuePair.Key == part)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		private int GetConsumableKey(ConsumablesInfo info)
		{
			foreach (KeyValuePair<int, ConsumablesInfo> keyValuePair in this.Consumables)
			{
				if (keyValuePair.Value.ItemClass == info.ItemClass)
				{
					return keyValuePair.Key;
				}
			}
			return -1;
		}

		[NonSerialized]
		private IDictionary<string, ConsumablesInfo> usedlist;
	}
}
