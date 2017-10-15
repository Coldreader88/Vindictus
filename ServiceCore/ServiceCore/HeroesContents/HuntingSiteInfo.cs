using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.HuntingSiteInfo")]
	public class HuntingSiteInfo
	{
		[Column(Storage = "_rowID", DbType = "Int NOT NULL")]
		public int rowID
		{
			get
			{
				return this._rowID;
			}
			set
			{
				if (this._rowID != value)
				{
					this._rowID = value;
				}
			}
		}

		[Column(Storage = "_HuntingSiteID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string HuntingSiteID
		{
			get
			{
				return this._HuntingSiteID;
			}
			set
			{
				if (this._HuntingSiteID != value)
				{
					this._HuntingSiteID = value;
				}
			}
		}

		[Column(Storage = "_TimeWindowLength", DbType = "Int NOT NULL")]
		public int TimeWindowLength
		{
			get
			{
				return this._TimeWindowLength;
			}
			set
			{
				if (this._TimeWindowLength != value)
				{
					this._TimeWindowLength = value;
				}
			}
		}

		[Column(Storage = "_TimeWindowSize", DbType = "Int NOT NULL")]
		public int TimeWindowSize
		{
			get
			{
				return this._TimeWindowSize;
			}
			set
			{
				if (this._TimeWindowSize != value)
				{
					this._TimeWindowSize = value;
				}
			}
		}

		[Column(Storage = "_AutoFishingTimeout", DbType = "Int NOT NULL")]
		public int AutoFishingTimeout
		{
			get
			{
				return this._AutoFishingTimeout;
			}
			set
			{
				if (this._AutoFishingTimeout != value)
				{
					this._AutoFishingTimeout = value;
				}
			}
		}

		private int _rowID;

		private string _HuntingSiteID;

		private int _TimeWindowLength;

		private int _TimeWindowSize;

		private int _AutoFishingTimeout;
	}
}
