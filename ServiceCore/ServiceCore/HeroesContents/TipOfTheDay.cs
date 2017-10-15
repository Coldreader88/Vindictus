using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TipOfTheDay")]
	public class TipOfTheDay
	{
		[Column(Storage = "_TipID", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string TipID
		{
			get
			{
				return this._TipID;
			}
			set
			{
				if (this._TipID != value)
				{
					this._TipID = value;
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

		private string _TipID;

		private string _Feature;
	}
}
