using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CustomizeColorInfo")]
	public class CustomizeColorInfo
	{
		[Column(Storage = "_Category", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if (this._Category != value)
				{
					this._Category = value;
				}
			}
		}

		[Column(Name = "[Order]", Storage = "_Order", DbType = "Int NOT NULL")]
		public int Order
		{
			get
			{
				return this._Order;
			}
			set
			{
				if (this._Order != value)
				{
					this._Order = value;
				}
			}
		}

		[Column(Storage = "_Color", DbType = "Int NOT NULL")]
		public int Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				if (this._Color != value)
				{
					this._Color = value;
				}
			}
		}

		[Column(Storage = "_Weight", DbType = "Int NOT NULL")]
		public int Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if (this._Weight != value)
				{
					this._Weight = value;
				}
			}
		}

		[Column(Storage = "_IsCash", DbType = "Bit NOT NULL")]
		public bool IsCash
		{
			get
			{
				return this._IsCash;
			}
			set
			{
				if (this._IsCash != value)
				{
					this._IsCash = value;
				}
			}
		}

		[Column(Storage = "_Description", DbType = "VarChar(100) NOT NULL", CanBeNull = false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if (this._Description != value)
				{
					this._Description = value;
				}
			}
		}

		[Column(Storage = "_CashShopType", DbType = "VarChar(50)")]
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

		private string _Category;

		private int _Order;

		private int _Color;

		private int _Weight;

		private bool _IsCash;

		private string _Description;

		private string _CashShopType;

		private string _Feature;
	}
}
