using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RandomItem")]
	public class RandomItem
	{
		[Column(Storage = "_ID", DbType = "Int NOT NULL")]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this._ID = value;
				}
			}
		}

		[Column(Storage = "_ServerCode", DbType = "Int")]
		public int? ServerCode
		{
			get
			{
				return this._ServerCode;
			}
			set
			{
				if (this._ServerCode != value)
				{
					this._ServerCode = value;
				}
			}
		}

		[Column(Storage = "_Description", DbType = "NChar(10)")]
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

		[Column(Storage = "_ItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_KeyItemClass", DbType = "NVarChar(50)")]
		public string KeyItemClass
		{
			get
			{
				return this._KeyItemClass;
			}
			set
			{
				if (this._KeyItemClass != value)
				{
					this._KeyItemClass = value;
				}
			}
		}

		[Column(Storage = "_Visual", DbType = "Int")]
		public int? Visual
		{
			get
			{
				return this._Visual;
			}
			set
			{
				if (this._Visual != value)
				{
					this._Visual = value;
				}
			}
		}

		[Column(Storage = "_BaitProbability", DbType = "Float")]
		public double? BaitProbability
		{
			get
			{
				return this._BaitProbability;
			}
			set
			{
				double? baitProbability = this._BaitProbability;
				double? num = value;
				if (baitProbability.GetValueOrDefault() != num.GetValueOrDefault() || baitProbability != null != (num != null))
				{
					this._BaitProbability = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(50)")]
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

		[Column(Storage = "_UseFeverPoint", DbType = "Int")]
		public int? UseFeverPoint
		{
			get
			{
				return this._UseFeverPoint;
			}
			set
			{
				if (this._UseFeverPoint != value)
				{
					this._UseFeverPoint = value;
				}
			}
		}

		[Column(Storage = "_FeverPoint", DbType = "Int")]
		public int? FeverPoint
		{
			get
			{
				return this._FeverPoint;
			}
			set
			{
				if (this._FeverPoint != value)
				{
					this._FeverPoint = value;
				}
			}
		}

		[Column(Storage = "_RareItemCount", DbType = "Int")]
		public int? RareItemCount
		{
			get
			{
				return this._RareItemCount;
			}
			set
			{
				if (this._RareItemCount != value)
				{
					this._RareItemCount = value;
				}
			}
		}

		[Column(Storage = "_NormalItemCount", DbType = "Int")]
		public int? NormalItemCount
		{
			get
			{
				return this._NormalItemCount;
			}
			set
			{
				if (this._NormalItemCount != value)
				{
					this._NormalItemCount = value;
				}
			}
		}

		private int _ID;

		private int? _ServerCode;

		private string _Description;

		private string _ItemClass;

		private string _KeyItemClass;

		private int? _Visual;

		private double? _BaitProbability;

		private string _Feature;

		private int? _UseFeverPoint;

		private int? _FeverPoint;

		private int? _RareItemCount;

		private int? _NormalItemCount;
	}
}
