using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PropItemInfo")]
	public class PropItemInfo
	{
		[Column(Storage = "_SectorID", DbType = "Int NOT NULL")]
		public int SectorID
		{
			get
			{
				return this._SectorID;
			}
			set
			{
				if (this._SectorID != value)
				{
					this._SectorID = value;
				}
			}
		}

		[Column(Storage = "_Class", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Class
		{
			get
			{
				return this._Class;
			}
			set
			{
				if (this._Class != value)
				{
					this._Class = value;
				}
			}
		}

		[Column(Storage = "_Type", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if (this._Type != value)
				{
					this._Type = value;
				}
			}
		}

		[Column(Storage = "_Amount", DbType = "BigInt NOT NULL")]
		public long Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if (this._Amount != value)
				{
					this._Amount = value;
				}
			}
		}

		[Column(Storage = "_Probability", DbType = "BigInt NOT NULL")]
		public long Probability
		{
			get
			{
				return this._Probability;
			}
			set
			{
				if (this._Probability != value)
				{
					this._Probability = value;
				}
			}
		}

		private int _SectorID;

		private string _Class;

		private string _Type;

		private long _Amount;

		private long _Probability;
	}
}
