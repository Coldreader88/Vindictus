using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnhanceStatInfo")]
	public class EnhanceStatInfo
	{
		[Column(Storage = "_EnhanceType", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string EnhanceType
		{
			get
			{
				return this._EnhanceType;
			}
			set
			{
				if (this._EnhanceType != value)
				{
					this._EnhanceType = value;
				}
			}
		}

		[Column(Storage = "_EnhanceLevel", DbType = "Int NOT NULL")]
		public int EnhanceLevel
		{
			get
			{
				return this._EnhanceLevel;
			}
			set
			{
				if (this._EnhanceLevel != value)
				{
					this._EnhanceLevel = value;
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

		private string _EnhanceType;

		private int _EnhanceLevel;

		private string _Stat;

		private string _ValueType;

		private float _Value;

		private string _Base;
	}
}
