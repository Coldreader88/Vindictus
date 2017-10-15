using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ItemAttributeInfo")]
	public class ItemAttributeInfo
	{
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

		[Column(Storage = "_AttributeName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string AttributeName
		{
			get
			{
				return this._AttributeName;
			}
			set
			{
				if (this._AttributeName != value)
				{
					this._AttributeName = value;
				}
			}
		}

		[Column(Storage = "_LocalizePrefix", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
		public string LocalizePrefix
		{
			get
			{
				return this._LocalizePrefix;
			}
			set
			{
				if (this._LocalizePrefix != value)
				{
					this._LocalizePrefix = value;
				}
			}
		}

		[Column(Storage = "_Modifier", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
		public string Modifier
		{
			get
			{
				return this._Modifier;
			}
			set
			{
				if (this._Modifier != value)
				{
					this._Modifier = value;
				}
			}
		}

		private int _Order;

		private string _AttributeName;

		private string _LocalizePrefix;

		private string _Modifier;
	}
}
