using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EnterRegion : IMessage
	{
		public int RegionCode { get; set; }

		public override string ToString()
		{
			return string.Format("EnterRegion({0})", this.RegionCode);
		}
	}
}
