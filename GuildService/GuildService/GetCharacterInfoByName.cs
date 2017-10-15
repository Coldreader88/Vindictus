using System;
using System.Data.Linq.Mapping;

namespace GuildService
{
	public class GetCharacterInfoByName
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

		[Column(Storage = "_ID", DbType = "BigInt NOT NULL")]
		public long ID
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

		[Column(Storage = "_CharacterID", DbType = "NVarChar(50)")]
		public string CharacterID
		{
			get
			{
				return this._CharacterID;
			}
			set
			{
				if (this._CharacterID != value)
				{
					this._CharacterID = value;
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

		[Column(Storage = "_BaseCharacter", DbType = "TinyInt NOT NULL")]
		public byte BaseCharacter
		{
			get
			{
				return this._BaseCharacter;
			}
			set
			{
				if (this._BaseCharacter != value)
				{
					this._BaseCharacter = value;
				}
			}
		}

		[Column(Storage = "_BaseCharacterName", DbType = "VarChar(6)")]
		public string BaseCharacterName
		{
			get
			{
				return this._BaseCharacterName;
			}
			set
			{
				if (this._BaseCharacterName != value)
				{
					this._BaseCharacterName = value;
				}
			}
		}

		[Column(Name = "[Level]", Storage = "_Level", DbType = "Int NOT NULL")]
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if (this._Level != value)
				{
					this._Level = value;
				}
			}
		}

		private int _NexonSN;

		private long _ID;

		private string _CharacterID;

		private int _CharacterSN;

		private byte _BaseCharacter;

		private string _BaseCharacterName;

		private int _Level;
	}
}
