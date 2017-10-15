using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Nexon.Nisms.Packets
{
	public class PurchaseItemEXRequest
	{
		public IPAddress RemoteIP { get; set; }

		public Reason Reason { get; set; }

		public string GameID { get; set; }

		public string UserID { get; set; }

		public int UserOID { get; set; }

		public string UserName { get; set; }

		public byte UserAge { get; set; }

		public string OrderID { get; set; }

		public PaymentType PaymentType { get; set; }

		public PaymentRule PaymentRule { get; set; }

		public int TotalAmount { get; set; }

		public ICollection<PurchaseItemEXRequest.Product> ProductArray { get; set; }

		private int CalculateStructureSize()
		{
			int num = this.RemoteIP.CalculateStructureSize() + ((byte)this.Reason).CalculateStructureSize() + this.GameID.CalculateStructureSize() + this.UserID.CalculateStructureSize() + this.UserOID.CalculateStructureSize() + this.UserName.CalculateStructureSize() + this.UserAge.CalculateStructureSize() + this.OrderID.CalculateStructureSize() + ((int)this.PaymentType).CalculateStructureSize() + ((byte)this.PaymentRule).CalculateStructureSize() + this.TotalAmount.CalculateStructureSize() + 0.CalculateStructureSize();
			int num2;
			if (this.ProductArray != null)
			{
				num2 = (from product in this.ProductArray
				select product.CalculateStructureSize()).Sum();
			}
			else
			{
				num2 = 0;
			}
			return num + num2;
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.PurchaseItemEX);
			result.Write(this.RemoteIP);
			result.Write((byte)this.Reason);
			result.Write(this.GameID);
			result.Write(this.UserID);
			result.Write(this.UserOID);
			result.Write(this.UserName);
			result.Write(this.UserAge);
			result.Write(this.OrderID);
			result.Write((int)this.PaymentType);
			result.Write((byte)this.PaymentRule);
			result.Write(this.TotalAmount);
			result.Write((byte)((this.ProductArray == null) ? 0 : this.ProductArray.Count));
			if (this.ProductArray != null)
			{
				foreach (PurchaseItemEXRequest.Product product in this.ProductArray)
				{
					result.Write(product.ProductNo);
					result.Write(product.OrderQuantity);
				}
			}
			return result;
		}

		public class Product
		{
			public int ProductNo { get; set; }

			public short OrderQuantity { get; set; }

			internal int CalculateStructureSize()
			{
				return this.ProductNo.CalculateStructureSize() + this.OrderQuantity.CalculateStructureSize();
			}
		}
	}
}
