using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RandomItemV2RareItemReset")]
	public class RandomItemV2RareItemReset
	{
		[Column(Storage = "_RareItemID", DbType = "Int NOT NULL")]
		public int RareItemID
		{
			get
			{
				return this._RareItemID;
			}
			set
			{
				if (this._RareItemID != value)
				{
					this._RareItemID = value;
				}
			}
		}

		[Column(Storage = "_ResetPeriod", DbType = "Int NOT NULL")]
		public int ResetPeriod
		{
			get
			{
				return this._ResetPeriod;
			}
			set
			{
				if (this._ResetPeriod != value)
				{
					this._ResetPeriod = value;
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

		private int _RareItemID;

		private int _ResetPeriod;

		private string _Feature;
	}
}
