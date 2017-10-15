using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.StatInfo")]
	public class StatInfo
	{
		[Column(Storage = "_Identifier", DbType = "Int NOT NULL")]
		public int Identifier
		{
			get
			{
				return this._Identifier;
			}
			set
			{
				if (this._Identifier != value)
				{
					this._Identifier = value;
				}
			}
		}

		[Column(Storage = "_StatName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string StatName
		{
			get
			{
				return this._StatName;
			}
			set
			{
				if (this._StatName != value)
				{
					this._StatName = value;
				}
			}
		}

		[Column(Storage = "_Description", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
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

		private int _Identifier;

		private string _StatName;

		private string _Description;
	}
}
