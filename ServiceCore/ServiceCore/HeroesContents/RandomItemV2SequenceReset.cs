using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RandomItemV2SequenceReset")]
	public class RandomItemV2SequenceReset
	{
		[Column(Storage = "_RandomItemID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string RandomItemID
		{
			get
			{
				return this._RandomItemID;
			}
			set
			{
				if (this._RandomItemID != value)
				{
					this._RandomItemID = value;
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

		private string _RandomItemID;

		private int _ResetPeriod;

		private string _Feature;
	}
}
