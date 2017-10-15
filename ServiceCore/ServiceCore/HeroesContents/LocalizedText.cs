using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.LocalizedText")]
	public class LocalizedText
	{
		[Column(Storage = "_Lang", DbType = "NVarChar(16) NOT NULL", CanBeNull = false)]
		public string Lang
		{
			get
			{
				return this._Lang;
			}
			set
			{
				if (this._Lang != value)
				{
					this._Lang = value;
				}
			}
		}

		[Column(Name = "[Key]", Storage = "_Key", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
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

		[Column(Storage = "_Value", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
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

		private string _Lang;

		private string _Key;

		private string _Value;
	}
}
