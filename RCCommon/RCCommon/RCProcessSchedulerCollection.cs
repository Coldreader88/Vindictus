using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem
{
	[Guid("6B026B7C-09F7-431b-B390-E093748218AE")]
	[Serializable]
	public class RCProcessSchedulerCollection : SynchronizedKeyedCollection<string, RCProcessScheduler>
	{
		protected override string GetKeyForItem(RCProcessScheduler item)
		{
			return item.Name;
		}
	}
}
