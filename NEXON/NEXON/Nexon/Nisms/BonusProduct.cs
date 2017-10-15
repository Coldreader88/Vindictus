using System;

namespace Nexon.Nisms
{
	[Serializable]
	public class BonusProduct
	{
		public int ProductNo { get; private set; }

		public string Extend { get; private set; }

		internal BonusProduct(ref Packet packet)
		{
			this.ProductNo = packet.ReadInt32();
			this.Extend = packet.ReadString();
		}
	}
}
