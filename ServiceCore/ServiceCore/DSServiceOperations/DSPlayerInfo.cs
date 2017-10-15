using System;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public class DSPlayerInfo
	{
		public long CID { get; set; }

		public long FID { get; set; }

		public int Level { get; set; }

		public DSPlayerInfo(long cid, long fid, int level)
		{
			this.CID = cid;
			this.FID = fid;
			this.Level = level;
		}
	}
}
