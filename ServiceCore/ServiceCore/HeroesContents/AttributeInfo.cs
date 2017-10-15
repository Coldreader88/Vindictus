using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AttributeInfo")]
	public class AttributeInfo
	{
		[Column(Storage = "_Attribute", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				if (this._Attribute != value)
				{
					this._Attribute = value;
				}
			}
		}

		[Column(Name = "[Desc]", Storage = "_Desc", DbType = "VarChar(256) NOT NULL", CanBeNull = false)]
		public string Desc
		{
			get
			{
				return this._Desc;
			}
			set
			{
				if (this._Desc != value)
				{
					this._Desc = value;
				}
			}
		}

		[Column(Storage = "_NameModifier", DbType = "VarChar(256) NOT NULL", CanBeNull = false)]
		public string NameModifier
		{
			get
			{
				return this._NameModifier;
			}
			set
			{
				if (this._NameModifier != value)
				{
					this._NameModifier = value;
				}
			}
		}

		private string _Attribute;

		private string _Desc;

		private string _NameModifier;
	}
}
