using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.QualityStatInfo")]
	public class QualityStatInfo
	{
		[Column(Storage = "_ItemType", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string ItemType
		{
			get
			{
				return this._ItemType;
			}
			set
			{
				if (this._ItemType != value)
				{
					this._ItemType = value;
				}
			}
		}

		[Column(Storage = "_Quality", DbType = "Int NOT NULL")]
		public int Quality
		{
			get
			{
				return this._Quality;
			}
			set
			{
				if (this._Quality != value)
				{
					this._Quality = value;
				}
			}
		}

		[Column(Storage = "_Stat", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Stat
		{
			get
			{
				return this._Stat;
			}
			set
			{
				if (this._Stat != value)
				{
					this._Stat = value;
				}
			}
		}

		[Column(Storage = "_ValueType", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string ValueType
		{
			get
			{
				return this._ValueType;
			}
			set
			{
				if (this._ValueType != value)
				{
					this._ValueType = value;
				}
			}
		}

		[Column(Storage = "_Value", DbType = "Real NOT NULL")]
		public float Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if (this._Value != value)
				{
					this._Value = value;
				}
			}
		}

		[Column(Storage = "_Base", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Base
		{
			get
			{
				return this._Base;
			}
			set
			{
				if (this._Base != value)
				{
					this._Base = value;
				}
			}
		}

		private string _ItemType;

		private int _Quality;

		private string _Stat;

		private string _ValueType;

		private float _Value;

		private string _Base;
	}
}
