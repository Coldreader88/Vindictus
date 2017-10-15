using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RecommendFriendListMessage : IMessage
	{
		public string friendName { get; set; }

		public List<string> recommenderList { get; set; }

		public override string ToString()
		{
			return string.Format("RecommendFriendListMessage[ FriendCID = {0}, Recommender = {1}", this.friendName, this.recommenderList);
		}
	}
}
