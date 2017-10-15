using System;
using ServiceCore.DSServiceOperations;
using Utility;

namespace DSService.WaitingQueue.Connection
{
	public class Dummy : IExternalOperation
	{
		public DSWaitingSystem Parent { get; set; }

		public Dummy(DSWaitingSystem parent)
		{
			this.Parent = parent;
		}

		public void LaunchShip(int serviceID, int dsid, string questID, long microPlayID, long partyID, long frontendID, bool isGiantRaid, bool isAdultMode, Action<bool> onComplete)
		{
			DSInfo dsinfo = this.Parent.DSStorage.DSMap.TryGetValue(dsid);
			if (dsinfo != null)
			{
				dsinfo.GetDSShip().Launched((long)(10000 + dsid));
			}
		}
	}
}
