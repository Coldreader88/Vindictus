using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace GuildService.API.HeroesAPI
{
	public class GuildGroupChangeMasterResult
	{
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

		[Column(Storage = "_GuildID", DbType = "VarChar(24) NOT NULL", CanBeNull = false)]
		public string GuildID
		{
			get
			{
				return this._GuildID;
			}
			set
			{
				if (this._GuildID != value)
				{
					this._GuildID = value;
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

		[Column(Storage = "_GuildName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string GuildName
		{
			get
			{
				return this._GuildName;
			}
			set
			{
				if (this._GuildName != value)
				{
					this._GuildName = value;
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

		[Column(Storage = "_dateClosed", DbType = "DateTime")]
		public DateTime? dateClosed
		{
			get
			{
				return this._dateClosed;
			}
			set
			{
				if (this._dateClosed != value)
				{
					this._dateClosed = value;
				}
			}
		}

		[Column(Storage = "_OwnerID", DbType = "BigInt")]
		public long? OwnerID
		{
			get
			{
				return this._OwnerID;
			}
			set
			{
				if (this._OwnerID != value)
				{
					this._OwnerID = value;
				}
			}
		}

		[Column(Storage = "_RealUserCount", DbType = "Int NOT NULL")]
		public int RealUserCount
		{
			get
			{
				return this._RealUserCount;
			}
			set
			{
				if (this._RealUserCount != value)
				{
					this._RealUserCount = value;
				}
			}
		}

		[Column(Storage = "_LastUpdate", DbType = "rowversion")]
		public Binary LastUpdate
		{
			get
			{
				return this._LastUpdate;
			}
			set
			{
				if (this._LastUpdate != value)
				{
					this._LastUpdate = value;
				}
			}
		}

		private int _GuildSN;

		private string _GuildID;

		private DateTime _dateCreate;

		private DateTime _dateLastModified;

		private string _GuildName;

		private string _Intro;

		private DateTime? _dateClosed;

		private long? _OwnerID;

		private int _RealUserCount;

		private Binary _LastUpdate;
	}
}
