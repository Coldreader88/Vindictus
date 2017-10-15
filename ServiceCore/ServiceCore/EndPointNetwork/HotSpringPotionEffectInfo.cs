using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HotSpringPotionEffectInfo : IMessage
	{
		public string PotionItemClass { get; set; }

		public string CharacterName { get; set; }

		public string GuildName { get; set; }

		public int ExpiredTime { get; set; }

		public int OtherPotionUsableTime { get; set; }

		public HotSpringPotionEffectInfo(string _potionItemClass, string _characterName, string _guildName, int _expiredTime, int _otherPotionUsableTime)
		{
			this.PotionItemClass = _potionItemClass;
			this.CharacterName = _characterName;
			this.GuildName = _guildName;
			this.ExpiredTime = _expiredTime;
			this.OtherPotionUsableTime = _otherPotionUsableTime;
		}
	}
}
