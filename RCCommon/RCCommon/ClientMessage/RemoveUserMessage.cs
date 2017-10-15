using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("1EC7601F-8800-4cc8-9AE9-622E886843EC")]
	[Serializable]
	public sealed class RemoveUserMessage
	{
		public string Account { get; private set; }

		public RemoveUserMessage(string id)
		{
			this.Account = id;
		}
	}
}
