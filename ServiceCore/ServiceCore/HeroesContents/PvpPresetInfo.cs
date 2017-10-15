using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PvpPresetInfo")]
	public class PvpPresetInfo
	{
		[Column(Storage = "_Map", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Map
		{
			get
			{
				return this._Map;
			}
			set
			{
				if (this._Map != value)
				{
					this._Map = value;
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

		[Column(Storage = "_Category", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if (this._Category != value)
				{
					this._Category = value;
				}
			}
		}

		[Column(Name = "[Key]", Storage = "_Key", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				if (this._Key != value)
				{
					this._Key = value;
				}
			}
		}

		[Column(Storage = "_Value", DbType = "Int NOT NULL")]
		public int Value
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

		private string _Map;

		private byte _CharacterClass;

		private string _Category;

		private string _Key;

		private int _Value;

		private string _Feature;
	}
}
