using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DefaultItemInfo")]
	public class DefaultItemInfo
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

		[Column(Storage = "_CharacterClass", DbType = "TinyInt NOT NULL")]
		public byte CharacterClass
		{
			get
			{
				return this._CharacterClass;
			}
			set
			{
				if (this._CharacterClass != value)
				{
					this._CharacterClass = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_ItemNum", DbType = "Int NOT NULL")]
		public int ItemNum
		{
			get
			{
				return this._ItemNum;
			}
			set
			{
				if (this._ItemNum != value)
				{
					this._ItemNum = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(128)")]
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

		private byte _CharacterClass;

		private string _ItemClass;

		private int _ItemNum;

		private string _Feature;
	}
}
