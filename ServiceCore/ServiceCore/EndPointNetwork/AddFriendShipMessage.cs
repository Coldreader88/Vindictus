using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AddFriendShipMessage : IMessage
	{
		public string CharacterName { get; set; }

		public AddFriendShipMessage(string charactername)
		{
			this.CharacterName = charactername;
		}
	}
}
