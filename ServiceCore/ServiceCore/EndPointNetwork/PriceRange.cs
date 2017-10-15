using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public struct PriceRange
	{
		public int Min { get; set; }

		public int Max { get; set; }

		public int Price { get; set; }
	}
}
