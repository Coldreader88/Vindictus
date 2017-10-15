using System;

namespace ServiceCore.EndPointNetwork.MicroPlay
{
	[Serializable]
	public sealed class AnswerFinishQuestMessage : IMessage
	{
		public bool FollowHost { get; set; }

		public override string ToString()
		{
			return string.Format("AnswerFinishQuestMessage[ {0} ]", this.FollowHost);
		}
	}
}
