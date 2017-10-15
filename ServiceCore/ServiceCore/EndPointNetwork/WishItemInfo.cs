using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WishItemInfo : IMessage
	{
		public long CID { get; set; }

		public int ProductNo { get; set; }

		public string ProductName { get; set; }

		public WishItemInfo(long cid, int productno, string productname)
		{
			this.CID = cid;
			this.ProductNo = productno;
			this.ProductName = productname;
		}

		public override string ToString()
		{
			string text = "";
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				"CID ",
				this.CID,
				"\n"
			});
			object obj2 = text;
			text = string.Concat(new object[]
			{
				obj2,
				"ProductNo ",
				this.ProductNo,
				"\n"
			});
			return text + "ProductName " + this.ProductName.ToString() + "\n";
		}
	}
}
