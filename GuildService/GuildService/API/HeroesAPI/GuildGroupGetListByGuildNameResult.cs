using System;
using System.Data.Linq.Mapping;

namespace GuildService.API.HeroesAPI
{
	public class GuildGroupGetListByGuildNameResult
	{
		[Column(Storage = "_CharacterSn_Master", DbType = "Int NOT NULL")]
		public int CharacterSn_Master
		{
			get
			{
				return this._CharacterSn_Master;
			}
			set
			{
				if (this._CharacterSn_Master != value)
				{
					this._CharacterSn_Master = value;
				}
			}
		}

		[Column(Storage = "_dtCreateDate", DbType = "DateTime NOT NULL")]
		public DateTime dtCreateDate
		{
			get
			{
				return this._dtCreateDate;
			}
			set
			{
				if (this._dtCreateDate != value)
				{
					this._dtCreateDate = value;
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

		[Column(Storage = "_NameInGroup_Master", DbType = "NVarChar(50)")]
		public string NameInGroup_Master
		{
			get
			{
				return this._NameInGroup_Master;
			}
			set
			{
				if (this._NameInGroup_Master != value)
				{
					this._NameInGroup_Master = value;
				}
			}
		}

		[Column(Storage = "_NexonSN_Master", DbType = "Int NOT NULL")]
		public int NexonSN_Master
		{
			get
			{
				return this._NexonSN_Master;
			}
			set
			{
				if (this._NexonSN_Master != value)
				{
					this._NexonSN_Master = value;
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

		private int _CharacterSn_Master;

		private DateTime _dtCreateDate;

		private string _GuildID;

		private string _GuildName;

		private int _GuildSN;

		private string _Intro;

		private string _NameInGroup_Master;

		private int _NexonSN_Master;

		private int _RealUserCount;
	}
}
