using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PetActiveTimePeriodInfo")]
	public class PetActiveTimePeriodInfo
	{
		[Column(Storage = "_PetID", DbType = "Int NOT NULL")]
		public int PetID
		{
			get
			{
				return this._PetID;
			}
			set
			{
				if (this._PetID != value)
				{
					this._PetID = value;
				}
			}
		}

		[Column(Storage = "_DateFrom", DbType = "DateTime NOT NULL")]
		public DateTime DateFrom
		{
			get
			{
				return this._DateFrom;
			}
			set
			{
				if (this._DateFrom != value)
				{
					this._DateFrom = value;
				}
			}
		}

		[Column(Storage = "_Period", DbType = "Int NOT NULL")]
		public int Period
		{
			get
			{
				return this._Period;
			}
			set
			{
				if (this._Period != value)
				{
					this._Period = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(64)")]
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

		private int _PetID;

		private DateTime _DateFrom;

		private int _Period;

		private string _Feature;
	}
}
