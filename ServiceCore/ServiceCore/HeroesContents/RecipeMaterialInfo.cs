using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RecipeMaterialInfo")]
	public class RecipeMaterialInfo
	{
		[Column(Storage = "_RecipeID", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string RecipeID
		{
			get
			{
				return this._RecipeID;
			}
			set
			{
				if (this._RecipeID != value)
				{
					this._RecipeID = value;
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

		[Column(Storage = "_Num", DbType = "Int NOT NULL")]
		public int Num
		{
			get
			{
				return this._Num;
			}
			set
			{
				if (this._Num != value)
				{
					this._Num = value;
				}
			}
		}

		[Column(Storage = "_Type", DbType = "TinyInt NOT NULL")]
		public byte Type
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

		private string _RecipeID;

		private string _ItemClass;

		private int _Num;

		private byte _Type;

		private string _Feature;
	}
}
