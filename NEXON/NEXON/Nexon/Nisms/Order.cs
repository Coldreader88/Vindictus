using System;

namespace Nexon.Nisms
{
	[Serializable]
	public class Order
	{
		public int OrderNo { get; private set; }

		public int ProductNo { get; private set; }

		public ProductKind ProductKind { get; private set; }

		public string ProductName { get; private set; }

		public string ProductID { get; private set; }

		public short ProductExpire { get; private set; }

		public short ProductPieces { get; private set; }

		public short OrderQuantity { get; private set; }

		public short RemainOrderQuantity { get; private set; }

		public bool IsPresent { get; private set; }

		public bool IsRead { get; private set; }

		public byte SenderServerNo { get; private set; }

		public string SenderGameID { get; private set; }

		public string SenderPresentMessage { get; private set; }

		public string ProductAttribute0 { get; private set; }

		public string ProductAttribute1 { get; private set; }

		public string ProductAttribute2 { get; private set; }

		public string ProductAttribute3 { get; private set; }

		public string ProductAttribute4 { get; private set; }

		public string ExtendValue { get; private set; }

		internal Order(ref Packet packet)
		{
			this.OrderNo = packet.ReadInt32();
			this.ProductNo = packet.ReadInt32();
			this.ProductKind = (ProductKind)packet.ReadByte();
			this.ProductName = packet.ReadString();
			this.ProductID = packet.ReadString();
			this.ProductExpire = packet.ReadInt16();
			this.ProductPieces = packet.ReadInt16();
			this.OrderQuantity = packet.ReadInt16();
			this.RemainOrderQuantity = packet.ReadInt16();
			this.IsPresent = packet.ReadBoolean();
			this.IsRead = packet.ReadBoolean();
			this.SenderServerNo = packet.ReadByte();
			this.SenderGameID = packet.ReadString();
			this.SenderPresentMessage = packet.ReadString();
			this.ProductAttribute0 = packet.ReadString();
			this.ProductAttribute1 = packet.ReadString();
			this.ProductAttribute2 = packet.ReadString();
			this.ProductAttribute3 = packet.ReadString();
			this.ProductAttribute4 = packet.ReadString();
			this.ExtendValue = packet.ReadString();
		}
	}
}
