using System;
using System.Collections.Generic;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EnhanceItemResultMessage : IMessage
	{
		public string Result { get; private set; }

		public long ItemID { get; private set; }

		public string PrevItemClass { get; private set; }

		public string EnhancedItemClass { get; private set; }

		public Dictionary<string, int> FailReward { get; private set; }

		private string ItemClassEx(string itemClass, int enhanceLevel)
		{
			if (enhanceLevel <= 0)
			{
				return itemClass;
			}
			return ItemClassExBuilder.Build(itemClass, enhanceLevel);
		}

		public EnhanceItemResultMessage(string result, long itemID, string previtemClass, string enhancedItemClass, Dictionary<string, int> failReward)
		{
			this.Result = result;
			this.ItemID = itemID;
			this.PrevItemClass = previtemClass;
			this.EnhancedItemClass = enhancedItemClass;
			this.FailReward = failReward;
		}

		public override string ToString()
		{
			return string.Format("EnhanceItemResultMessage[ Result = {0} EnhancedItemClass = {1} ]", this.Result, this.EnhancedItemClass);
		}
	}
}
