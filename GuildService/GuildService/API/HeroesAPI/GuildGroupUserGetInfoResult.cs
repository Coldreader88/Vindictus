using System;
using System.Data.Linq.Mapping;

namespace GuildService.API.HeroesAPI
{
	public class GuildGroupUserGetInfoResult
	{
		[Column(Storage = "_NexonSN", DbType = "Int NOT NULL")]
		public int NexonSN
		{
			get
			{
				return this._NexonSN;
			}
			set
			{
				if (this._NexonSN != value)
				{
					this._NexonSN = value;
				}
			}
		}

		[Column(Storage = "_NameInGroup_User", DbType = "NVarChar(50)")]
		public string NameInGroup_User
		{
			get
			{
				return this._NameInGroup_User;
			}
			set
			{
				if (this._NameInGroup_User != value)
				{
					this._NameInGroup_User = value;
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

		[Column(Storage = "_emGroupUserType", DbType = "TinyInt NOT NULL")]
		public byte emGroupUserType
		{
			get
			{
				return this._emGroupUserType;
			}
			set
			{
				if (this._emGroupUserType != value)
				{
					this._emGroupUserType = value;
				}
			}
		}

		[Column(Storage = "_dtLastLoginTimeDate", DbType = "Int")]
		public int? dtLastLoginTimeDate
		{
			get
			{
				return this._dtLastLoginTimeDate;
			}
			set
			{
				if (this._dtLastLoginTimeDate != value)
				{
					this._dtLastLoginTimeDate = value;
				}
			}
		}

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

		[Column(Storage = "_GuildIntro", DbType = "NVarChar(200)")]
		public string GuildIntro
		{
			get
			{
				return this._GuildIntro;
			}
			set
			{
				if (this._GuildIntro != value)
				{
					this._GuildIntro = value;
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

		private int _NexonSN;

		private string _NameInGroup_User;

		private int _GuildSN;

		private byte _emGroupUserType;

		private int? _dtLastLoginTimeDate;

		private string _CharacterName;

		private string _GuildID;

		private string _GuildName;

		private string _GuildIntro;

		private int _NexonSN_Master;

		private string _NameInGroup_Master;

		private int _RealUserCount;
	}
}
