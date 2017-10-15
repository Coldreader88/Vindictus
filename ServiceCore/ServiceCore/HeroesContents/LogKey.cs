using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.LogKey")]
	public class LogKey
	{
		[Column(Name = "[Table]", Storage = "_Table", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Table
		{
			get
			{
				return this._Table;
			}
			set
			{
				if (this._Table != value)
				{
					this._Table = value;
				}
			}
		}

		[Column(Name = "[Column]", Storage = "_Column", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Column
		{
			get
			{
				return this._Column;
			}
			set
			{
				if (this._Column != value)
				{
					this._Column = value;
				}
			}
		}

		[Column(Storage = "_EnumValue", DbType = "Int NOT NULL")]
		public int EnumValue
		{
			get
			{
				return this._EnumValue;
			}
			set
			{
				if (this._EnumValue != value)
				{
					this._EnumValue = value;
				}
			}
		}

		[Column(Storage = "_EnumString", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string EnumString
		{
			get
			{
				return this._EnumString;
			}
			set
			{
				if (this._EnumString != value)
				{
					this._EnumString = value;
				}
			}
		}

		private string _Table;

		private string _Column;

		private int _EnumValue;

		private string _EnumString;
	}
}
