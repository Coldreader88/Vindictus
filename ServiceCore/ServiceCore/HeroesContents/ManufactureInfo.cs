using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ManufactureInfo")]
	public class ManufactureInfo
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

		[Column(Storage = "_Grade", DbType = "Int NOT NULL")]
		public int Grade
		{
			get
			{
				return this._Grade;
			}
			set
			{
				if (this._Grade != value)
				{
					this._Grade = value;
				}
			}
		}

		[Column(Storage = "_ExperienceMin", DbType = "Int NOT NULL")]
		public int ExperienceMin
		{
			get
			{
				return this._ExperienceMin;
			}
			set
			{
				if (this._ExperienceMin != value)
				{
					this._ExperienceMin = value;
				}
			}
		}

		[Column(Storage = "_ExperienceMax", DbType = "Int NOT NULL")]
		public int ExperienceMax
		{
			get
			{
				return this._ExperienceMax;
			}
			set
			{
				if (this._ExperienceMax != value)
				{
					this._ExperienceMax = value;
				}
			}
		}

		private string _ManufactureID;

		private int _Grade;

		private int _ExperienceMin;

		private int _ExperienceMax;
	}
}
