using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem
{
	[Guid("B6295867-A7FA-4a3a-8C5B-8A03718E44D9")]
	[Serializable]
	public class RCProcessCollection : SynchronizedKeyedCollection<string, RCProcess>
	{
		protected override string GetKeyForItem(RCProcess item)
		{
			return item.Name;
		}
	}
}
