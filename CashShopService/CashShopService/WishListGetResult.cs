using System;
using System.Data.Linq.Mapping;

namespace CashShopService
{
	public class WishListGetResult
	{
		[Column(Storage = "_CID", DbType = "BigInt NOT NULL")]
		public long CID
		{
			get
			{
				return this._CID;
			}
			set
			{
				if (this._CID != value)
				{
					this._CID = value;
				}
			}
		}

		[Column(Storage = "_ProductNo", DbType = "Int NOT NULL")]
		public int ProductNo
		{
			get
			{
				return this._ProductNo;
			}
			set
			{
				if (this._ProductNo != value)
				{
					this._ProductNo = value;
				}
			}
		}

		[Column(Storage = "_ProductName", DbType = "NVarChar(64) NOT NULL", CanBeNull = false)]
		public string ProductName
		{
			get
			{
				return this._ProductName;
			}
			set
			{
				if (this._ProductName != value)
				{
					this._ProductName = value;
				}
			}
		}

		private long _CID;

		private int _ProductNo;

		private string _ProductName;
	}
}
