using System;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.WaitingQueue.Connection
{
	public class ExternalService : IExternalOperation
	{
		public void LaunchShip(int serviceID, int dsid, string questID, long microPlayID, long partyID, long frontendID, bool isGiantRaid, bool isAdultMode, Action<bool> onComplete)
		{
			LaunchDS launchDS = new LaunchDS(dsid, questID, microPlayID, partyID, frontendID, isGiantRaid, isAdultMode);
			launchDS.OnComplete += delegate(Operation _)
			{
				onComplete(true);
			};
			launchDS.OnFail += delegate(Operation _)
			{
				onComplete(false);
			};
			DSService.Instance.RequestOperation(serviceID, launchDS);
		}
	}
}
