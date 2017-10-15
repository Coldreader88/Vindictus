using System;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GetItemMessage : IMessage
	{
		public string PlayerName { get; set; }

		public string ItemClass { get; set; }

		public int Count { get; set; }

		public int CoreType { get; set; }

		public bool Lucky { get; set; }

		public int LuckBonus { get; set; }

		public int GiveItemResult { get; set; }

		public GetItemMessage(string playerName, string ItemClass, int count, int coreType, bool Lucky, int LuckBonus, GiveItem.ResultEnum GiveItemResult)
		{
			this.PlayerName = playerName;
			this.ItemClass = ItemClass;
			this.Lucky = Lucky;
			this.LuckBonus = LuckBonus;
			this.GiveItemResult = (int)GiveItemResult;
			this.Count = count;
			this.CoreType = coreType;
		}

		public override string ToString()
		{
			return string.Format("GetItemMessage[ player = {0} lucky = {1} itemClass = {2} ]", this.PlayerName, this.Lucky, this.ItemClass);
		}
	}
}
