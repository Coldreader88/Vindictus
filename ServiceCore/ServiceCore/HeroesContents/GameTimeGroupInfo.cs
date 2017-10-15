using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.GameTimeGroupInfo")]
	public class GameTimeGroupInfo
	{
		[Column(Storage = "_BSPName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string BSPName
		{
			get
			{
				return this._BSPName;
			}
			set
			{
				if (this._BSPName != value)
				{
					this._BSPName = value;
				}
			}
		}

		[Column(Storage = "_GroupID", DbType = "TinyInt NOT NULL")]
		public byte GroupID
		{
			get
			{
				return this._GroupID;
			}
			set
			{
				if (this._GroupID != value)
				{
					this._GroupID = value;
				}
			}
		}

		[Column(Storage = "_MinTime", DbType = "TinyInt NOT NULL")]
		public byte MinTime
		{
			get
			{
				return this._MinTime;
			}
			set
			{
				if (this._MinTime != value)
				{
					this._MinTime = value;
				}
			}
		}

		[Column(Storage = "_MaxTime", DbType = "TinyInt NOT NULL")]
		public byte MaxTime
		{
			get
			{
				return this._MaxTime;
			}
			set
			{
				if (this._MaxTime != value)
				{
					this._MaxTime = value;
				}
			}
		}

		private string _BSPName;

		private byte _GroupID;

		private byte _MinTime;

		private byte _MaxTime;
	}
}
