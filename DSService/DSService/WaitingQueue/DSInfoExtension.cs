using System;
using ServiceCore.DSServiceOperations;
using Utility;

namespace DSService.WaitingQueue
{
	public static class DSInfoExtension
	{
		public static DSShip GetDSShip(this DSInfo info)
		{
			return DSService.Instance.DSWaitingSystem.DSStorage.DSShipMap.TryGetValue(info.DSID);
		}

		public static void SetDSShip(this DSInfo info, DSShip dsShip)
		{
			if (dsShip == null)
			{
				DSService.Instance.DSWaitingSystem.DSStorage.DSShipMap.Remove(info.DSID);
				return;
			}
			DSService.Instance.DSWaitingSystem.DSStorage.DSShipMap.Add(info.DSID, dsShip);
		}
	}
}
