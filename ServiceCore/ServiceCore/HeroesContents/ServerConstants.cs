using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ServerConstants")]
	public class ServerConstants
	{
		[Column(Storage = "_Property", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Property
		{
			get
			{
				return this._Property;
			}
			set
			{
				if (this._Property != value)
				{
					this._Property = value;
				}
			}
		}

		[Column(Storage = "_type", DbType = "NVarChar(10) NOT NULL", CanBeNull = false)]
		public string type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (this._type != value)
				{
					this._type = value;
				}
			}
		}

		[Column(Storage = "_Value", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string Value
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

		private string _Property;

		private string _type;

		private string _Value;
	}
}
