using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PvpInfo")]
	public class PvpInfo
	{
		[Column(Storage = "_PvpMode", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string PvpMode
		{
			get
			{
				return this._PvpMode;
			}
			set
			{
				if (this._PvpMode != value)
				{
					this._PvpMode = value;
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

		[Column(Storage = "_Value", DbType = "VarChar(1024) NOT NULL", CanBeNull = false)]
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

		private string _PvpMode;

		private string _Key;

		private string _Value;

		private string _Feature;
	}
}
