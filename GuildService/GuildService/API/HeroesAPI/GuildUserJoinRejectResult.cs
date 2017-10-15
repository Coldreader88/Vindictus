using System;
using System.Data.Linq.Mapping;

namespace GuildService.API.HeroesAPI
{
	public class GuildUserJoinRejectResult
	{
		[Column(Storage = "_CID", DbType = "BigInt NOT NULL")]
		public long CID
		{
			get
			{
				return this._CID;
			}
			set
			{
				if (this._CID != value)
				{
					this._CID = value;
				}
			}
		}

		[Column(Storage = "_GuildSN", DbType = "Int NOT NULL")]
		public int GuildSN
		{
			get
			{
				return this._GuildSN;
			}
			set
			{
				if (this._GuildSN != value)
				{
					this._GuildSN = value;
				}
			}
		}

		[Column(Storage = "_Intro", DbType = "NVarChar(200)")]
		public string Intro
		{
			get
			{
				return this._Intro;
			}
			set
			{
				if (this._Intro != value)
				{
					this._Intro = value;
				}
			}
		}

		[Column(Storage = "_dateCreate", DbType = "DateTime NOT NULL")]
		public DateTime dateCreate
		{
			get
			{
				return this._dateCreate;
			}
			set
			{
				if (this._dateCreate != value)
				{
					this._dateCreate = value;
				}
			}
		}

		[Column(Storage = "_dateLastModified", DbType = "DateTime NOT NULL")]
		public DateTime dateLastModified
		{
			get
			{
				return this._dateLastModified;
			}
			set
			{
				if (this._dateLastModified != value)
				{
					this._dateLastModified = value;
				}
			}
		}

		[Column(Storage = "_codeGroupUserType", DbType = "TinyInt NOT NULL")]
		public byte codeGroupUserType
		{
			get
			{
				return this._codeGroupUserType;
			}
			set
			{
				if (this._codeGroupUserType != value)
				{
					this._codeGroupUserType = value;
				}
			}
		}

		private long _CID;

		private int _GuildSN;

		private string _Intro;

		private DateTime _dateCreate;

		private DateTime _dateLastModified;

		private byte _codeGroupUserType;
	}
}
