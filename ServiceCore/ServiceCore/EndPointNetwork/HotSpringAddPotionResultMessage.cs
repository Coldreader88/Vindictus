using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HotSpringAddPotionResultMessage : IMessage
	{
		public HotSpringAddPotionResult Result { get; set; }

		public int TownID { get; set; }

		public string PrevPotionItemClass { get; set; }

		public HotSpringPotionEffectInfo hotSpringPotionEffectInfos { get; set; }

		public HotSpringAddPotionResultMessage(HotSpringAddPotionResult _result, int _townID, string _prevPotionItemClass = null, string _potionItemClass = "", string _characterName = "", string _guildName = "", int _expiredTime = -1, int _otherPotionUsableTime = -1)
		{
			this.Result = _result;
			this.TownID = _townID;
			this.PrevPotionItemClass = _prevPotionItemClass;
			this.hotSpringPotionEffectInfos = new HotSpringPotionEffectInfo(_potionItemClass, _characterName, _guildName, _expiredTime, _otherPotionUsableTime);
		}
	}
}
