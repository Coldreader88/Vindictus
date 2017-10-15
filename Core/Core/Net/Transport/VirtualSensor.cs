using System;
using System.Collections.Generic;

namespace Devcat.Core.Net.Transport
{
	public class VirtualSensor
	{
		public VirtualClient VirtualClient
		{
			get
			{
				return this.virtualClient;
			}
		}

		public VirtualSensor(VirtualClient virtualClient)
		{
			this.virtualClient = virtualClient;
			this.versionList = new SortedDictionary<int, long>();
		}

		internal void SetVersion(int stationID, long version)
		{
			if (this.versionList.ContainsKey(stationID))
			{
				this.versionList[stationID] = version;
				return;
			}
			lock (this.versionList)
			{
				this.versionList[stationID] = version;
			}
		}

		internal long GetVersion(int stationID)
		{
			long result;
			this.versionList.TryGetValue(stationID, out result);
			return result;
		}

		private VirtualClient virtualClient;

		private SortedDictionary<int, long> versionList;
	}
}
