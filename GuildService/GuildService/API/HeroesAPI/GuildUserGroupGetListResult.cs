using System;
using System.Data.Linq.Mapping;

namespace GuildService.API.HeroesAPI
{
	public class GuildUserGroupGetListResult
	{
		[Column(Storage = "_CharacterName", DbType = "NVarChar(50)")]
		public string CharacterName
		{
			get
			{
				return this._CharacterName;
			}
			set
			{
				if (this._CharacterName != value)
				{
					this._CharacterName = value;
				}
			}
		}

		[Column(Storage = "_CharacterSN", DbType = "Int NOT NULL")]
		public int CharacterSN
		{
			get
			{
				return this._CharacterSN;
			}
			set
			{
				if (this._CharacterSN != value)
				{
					this._CharacterSN = value;
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

		[Column(Storage = "_GroupUserType", DbType = "TinyInt NOT NULL")]
		public byte GroupUserType
		{
			get
			{
				return this._GroupUserType;
			}
			set
			{
				if (this._GroupUserType != value)
				{
					this._GroupUserType = value;
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

		[Column(Storage = "_NameInGroup", DbType = "NVarChar(50)")]
		public string NameInGroup
		{
			get
			{
				return this._NameInGroup;
			}
			set
			{
				if (this._NameInGroup != value)
				{
					this._NameInGroup = value;
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

		[Column(Storage = "_co", DbType = "BigInt")]
		public long? co
		{
			get
			{
				return this._co;
			}
			set
			{
				if (this._co != value)
				{
					this._co = value;
				}
			}
		}

		private string _CharacterName;

		private int _CharacterSN;

		private DateTime _dateCreate;

		private byte _GroupUserType;

		private string _GuildName;

		private int _GuildSN;

		private string _GuildID;

		private string _Intro;

		private string _NameInGroup;

		private string _NameInGroup_Master;

		private int _NexonSN_Master;

		private int _RealUserCount;

		private long? _co;
	}
}
