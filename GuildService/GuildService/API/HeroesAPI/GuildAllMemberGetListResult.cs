using System;
using System.Data.Linq.Mapping;

namespace GuildService.API.HeroesAPI
{
	public class GuildAllMemberGetListResult
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

		private byte _emGroupUserType;

		private int _GuildSN;

		private string _Intro;

		private string _NameInGroup;

		private int _NexonSN;

		private long? _co;
	}
}
