using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PropItemWeightInfo")]
	public class PropItemWeightInfo
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

		[Column(Storage = "_DropTableName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string DropTableName
		{
			get
			{
				return this._DropTableName;
			}
			set
			{
				if (this._DropTableName != value)
				{
					this._DropTableName = value;
				}
			}
		}

		[Column(Storage = "_MaterialGroup", DbType = "NVarChar(50)")]
		public string MaterialGroup
		{
			get
			{
				return this._MaterialGroup;
			}
			set
			{
				if (this._MaterialGroup != value)
				{
					this._MaterialGroup = value;
				}
			}
		}

		[Column(Storage = "_Type", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if (this._Type != value)
				{
					this._Type = value;
				}
			}
		}

		[Column(Storage = "_Item", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Item
		{
			get
			{
				return this._Item;
			}
			set
			{
				if (this._Item != value)
				{
					this._Item = value;
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

		private int _ID;

		private string _DropTableName;

		private string _MaterialGroup;

		private string _Type;

		private string _Item;

		private int _Weight;

		private string _Feature;
	}
}
