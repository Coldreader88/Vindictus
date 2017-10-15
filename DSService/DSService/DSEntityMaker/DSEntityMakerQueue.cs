using System;
using ServiceCore.DSServiceOperations;

namespace DSService.DSEntityMaker
{
	public class DSEntityMakerQueue
	{
		public DSType DSType { get; set; }

		public int PVPServiceID { get; set; }

		public DSEntityMakerQueue(long id, DSType dsType)
		{
			this.ID = id;
			this.DSType = dsType;
			this.PVPServiceID = 0;
		}

		public DSEntityMakerQueue(long id, DSType dsType, int pvpServiceID)
		{
			this.ID = id;
			this.DSType = dsType;
			this.PVPServiceID = pvpServiceID;
		}

		public long ID;
	}
}
