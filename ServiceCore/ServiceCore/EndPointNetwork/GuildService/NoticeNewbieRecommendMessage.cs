using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class NoticeNewbieRecommendMessage : IMessage
	{
		public NoticeNewbieRecommendMessage(string name, long id)
		{
			this.RequestUserName = name;
			this.ShipID = id;
		}

		public string RequestUserName { get; set; }

		public long ShipID { get; set; }

		public override string ToString()
		{
			return string.Format("{0}, {1}", this.RequestUserName, this.ShipID.ToString());
		}
	}
}
