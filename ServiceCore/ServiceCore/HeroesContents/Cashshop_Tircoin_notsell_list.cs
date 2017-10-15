using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.Cashshop_Tircoin_notsell_list")]
	public class Cashshop_Tircoin_notsell_list
	{
		[Column(Storage = "_CashShopType", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string CashShopType
		{
			get
			{
				return this._CashShopType;
			}
			set
			{
				if (this._CashShopType != value)
				{
					this._CashShopType = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "VarChar(150) NOT NULL", CanBeNull = false)]
		public string ItemClass
		{
			get
			{
				return this._ItemClass;
			}
			set
			{
				if (this._ItemClass != value)
				{
					this._ItemClass = value;
				}
			}
		}

		[Column(Storage = "_Amount", DbType = "Int")]
		public int? Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if (this._Amount != value)
				{
					this._Amount = value;
				}
			}
		}

		[Column(Storage = "_Duration", DbType = "Int")]
		public int? Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				if (this._Duration != value)
				{
					this._Duration = value;
				}
			}
		}

		[Column(Storage = "_Price", DbType = "Int")]
		public int? Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if (this._Price != value)
				{
					this._Price = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(50)")]
		public string Feature
		{
			get
			{
				return this._Feature;
			}
			set
			{
				if (this._Feature != value)
				{
					this._Feature = value;
				}
			}
		}

		private string _CashShopType;

		private string _ItemClass;

		private int? _Amount;

		private int? _Duration;

		private int? _Price;

		private string _Feature;
	}
}
