using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GameInfo
	{
		public string Name { get; set; }

		public string Map { get; set; }

		public string GameDir { get; set; }

		public string Description { get; set; }

		public int HostID { get; set; }

		public string DSIP { get; set; }

		public int DSPort { get; set; }

		public override string ToString()
		{
			return string.Format("GameInfo(Name = {0} Map = {1} HostID = {2} DSIP = {3} DSPort = {4} )", new object[]
			{
				this.Name,
				this.Map,
				this.HostID,
				this.DSIP,
				this.DSPort
			});
		}
	}
}
