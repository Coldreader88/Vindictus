using System;
using System.Collections.Generic;
using Utility;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ItemDropEntityInfo
	{
		public string EntityName { get; private set; }

		public IList<ItemDropInfo> DropItemList { get; private set; }

		public ItemDropEntityInfo(string ename)
		{
			this.EntityName = ename;
			this.DropItemList = new List<ItemDropInfo>();
		}

		public void AddItem(bool lucky, string itemClass, bool isStoryDrop)
		{
			this.DropItemList.Add(new ItemDropInfo(lucky, itemClass, isStoryDrop, this.EntityName));
		}

		public bool RemoveItem(string itemClass)
		{
			foreach (ItemDropInfo itemDropInfo in this.DropItemList)
			{
				if (itemDropInfo.ItemClass == itemClass)
				{
					if (this.DropItemList.Remove(itemDropInfo))
					{
						return true;
					}
					Log<ItemDropInfo>.Logger.ErrorFormat("아이템 클래스가 존재하지만 리스트에서 삭제되지 않았습니다. [{0} : {1}]", this.EntityName, itemClass);
				}
			}
			return false;
		}
	}
}
