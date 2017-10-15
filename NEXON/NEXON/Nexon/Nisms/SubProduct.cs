using System;

namespace Nexon.Nisms
{
	[Serializable]
	public class SubProduct
	{
		public int ProductNo { get; private set; }

		public string ProductName { get; private set; }

		public string ProductID { get; private set; }

		public short ProductExpire { get; private set; }

		public short ProductPieces { get; private set; }

		public string ProductAttribute0 { get; private set; }

		public string ProductAttribute1 { get; private set; }

		public string ProductAttribute2 { get; private set; }

		public string ProductAttribute3 { get; private set; }

		public string ProductAttribute4 { get; private set; }

		internal SubProduct(ref Packet packet)
		{
			this.ProductNo = packet.ReadInt32();
			this.ProductName = packet.ReadString();
			this.ProductID = packet.ReadString();
			this.ProductExpire = packet.ReadInt16();
			this.ProductPieces = packet.ReadInt16();
			this.ProductAttribute0 = packet.ReadString();
			this.ProductAttribute1 = packet.ReadString();
			this.ProductAttribute2 = packet.ReadString();
			this.ProductAttribute3 = packet.ReadString();
			this.ProductAttribute4 = packet.ReadString();
		}
	}
}
