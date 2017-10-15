using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateFriendshipPointMessage : IMessage
	{
		public int Point { get; set; }

		public UpdateFriendshipPointMessage(int point)
		{
			this.Point = point;
		}
	}
}
