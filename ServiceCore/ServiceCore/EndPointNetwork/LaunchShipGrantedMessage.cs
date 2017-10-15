using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LaunchShipGrantedMessage : IMessage
	{
		public string QuestID { get; set; }

		public byte AdultRule { get; set; }

		public byte IsPracticeMode { get; set; }

		public GameJoinMemberInfo HostInfo { get; set; }

		public LaunchShipGrantedMessage(string questID, byte adultRule, byte isPracticeMode, GameJoinMemberInfo hostInfo)
		{
			this.QuestID = questID;
			this.AdultRule = adultRule;
			this.IsPracticeMode = isPracticeMode;
			this.HostInfo = hostInfo;
		}

		public override string ToString()
		{
			return string.Format("LaunchShipGrantedMessage [ hostInfo = {0} ]", this.HostInfo);
		}
	}
}
