using System;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public class DSInfo
	{
		public int DSID { get; set; }

		public int ServiceID { get; set; }

		public DSType DSType { get; set; }

		public DSInfo(int dsID, int serviceID, DSType dsType)
		{
			this.DSID = dsID;
			this.ServiceID = serviceID;
			this.DSType = dsType;
		}
	}
}
