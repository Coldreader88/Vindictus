using System;

namespace DSService.WaitingQueue.Connection
{
	public interface IExternalOperation
	{
		void LaunchShip(int serviceID, int dsid, string questID, long microPlayID, long partyID, long frontendID, bool isGiantRaid, bool isAdultMode, Action<bool> onComplete);
	}
}
