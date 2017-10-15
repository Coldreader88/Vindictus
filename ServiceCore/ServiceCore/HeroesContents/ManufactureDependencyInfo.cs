using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ManufactureDependencyInfo")]
	public class ManufactureDependencyInfo
	{
		[Column(Storage = "_ManufactureID", DbType = "NVarChar(16) NOT NULL", CanBeNull = false)]
		public string ManufactureID
		{
			get
			{
				return this._ManufactureID;
			}
			set
			{
				if (this._ManufactureID != value)
				{
					this._ManufactureID = value;
				}
			}
		}

		[Column(Storage = "_RequiredManufactureID", DbType = "NVarChar(16) NOT NULL", CanBeNull = false)]
		public string RequiredManufactureID
		{
			get
			{
				return this._RequiredManufactureID;
			}
			set
			{
				if (this._RequiredManufactureID != value)
				{
					this._RequiredManufactureID = value;
				}
			}
		}

		[Column(Storage = "_RequiredExperience", DbType = "Int NOT NULL")]
		public int RequiredExperience
		{
			get
			{
				return this._RequiredExperience;
			}
			set
			{
				if (this._RequiredExperience != value)
				{
					this._RequiredExperience = value;
				}
			}
		}

		private string _ManufactureID;

		private string _RequiredManufactureID;

		private int _RequiredExperience;
	}
}
