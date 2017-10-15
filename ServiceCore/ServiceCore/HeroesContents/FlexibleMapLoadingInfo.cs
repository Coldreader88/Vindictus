using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.FlexibleMapLoadingInfo")]
	public class FlexibleMapLoadingInfo
	{
		[Column(Storage = "_ID", DbType = "TinyInt NOT NULL")]
		public byte ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this._ID = value;
				}
			}
		}

		[Column(Storage = "_IsOccurrence", DbType = "Bit NOT NULL")]
		public bool IsOccurrence
		{
			get
			{
				return this._IsOccurrence;
			}
			set
			{
				if (this._IsOccurrence != value)
				{
					this._IsOccurrence = value;
				}
			}
		}

		[Column(Storage = "_RegionName", DbType = "NVarChar(30) NOT NULL", CanBeNull = false)]
		public string RegionName
		{
			get
			{
				return this._RegionName;
			}
			set
			{
				if (this._RegionName != value)
				{
					this._RegionName = value;
				}
			}
		}

		[Column(Storage = "_StoryLineID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string StoryLineID
		{
			get
			{
				return this._StoryLineID;
			}
			set
			{
				if (this._StoryLineID != value)
				{
					this._StoryLineID = value;
				}
			}
		}

		[Column(Storage = "_Phase", DbType = "NVarChar(30) NOT NULL", CanBeNull = false)]
		public string Phase
		{
			get
			{
				return this._Phase;
			}
			set
			{
				if (this._Phase != value)
				{
					this._Phase = value;
				}
			}
		}

		private byte _ID;

		private bool _IsOccurrence;

		private string _RegionName;

		private string _StoryLineID;

		private string _Phase;
	}
}
