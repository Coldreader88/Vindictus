using System;
using System.Collections.Generic;

namespace RemoteControlSystem.Server
{
	[Serializable]
	public class UserCollection : SynchronizedKeyedCollection<int, User>
	{
		protected override int GetKeyForItem(User user)
		{
			return user.ClientId;
		}
	}
}
