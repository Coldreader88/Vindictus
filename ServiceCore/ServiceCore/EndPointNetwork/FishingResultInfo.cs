using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class FishingResultInfo
	{
		public string FishID { get; set; }

		public int Size { get; set; }

		public int Count { get; set; }

		public override string ToString()
		{
			return string.Format("FishingResultInfo {0} {1} {2}", this.FishID, this.Size, this.Count);
		}
	}
}
