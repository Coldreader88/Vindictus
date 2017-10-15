using System;
using System.Data.Linq.Mapping;

namespace GuildService
{
	public class GetGuildStorageItemLogs
	{
		[Column(Storage = "_CharacterName", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_OperationType", DbType = "TinyInt NOT NULL")]
		public byte OperationType
		{
			get
			{
				return this._OperationType;
			}
			set
			{
				if (this._OperationType != value)
				{
					this._OperationType = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string ItemClass
		{
			get
			{
				return this._ItemClass;
			}
			set
			{
				if (this._ItemClass != value)
				{
					this._ItemClass = value;
				}
			}
		}

		[Column(Storage = "_Count", DbType = "Int NOT NULL")]
		public int Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				if (this._Count != value)
				{
					this._Count = value;
				}
			}
		}

		[Column(Storage = "_Datestamp", DbType = "Int NOT NULL")]
		public int Datestamp
		{
			get
			{
				return this._Datestamp;
			}
			set
			{
				if (this._Datestamp != value)
				{
					this._Datestamp = value;
				}
			}
		}

		[Column(Storage = "_Timestamp", DbType = "Int NOT NULL")]
		public int Timestamp
		{
			get
			{
				return this._Timestamp;
			}
			set
			{
				if (this._Timestamp != value)
				{
					this._Timestamp = value;
				}
			}
		}

		[Column(Storage = "_Color1", DbType = "Int NOT NULL")]
		public int Color1
		{
			get
			{
				return this._Color1;
			}
			set
			{
				if (this._Color1 != value)
				{
					this._Color1 = value;
				}
			}
		}

		[Column(Storage = "_Color2", DbType = "Int NOT NULL")]
		public int Color2
		{
			get
			{
				return this._Color2;
			}
			set
			{
				if (this._Color2 != value)
				{
					this._Color2 = value;
				}
			}
		}

		[Column(Storage = "_Color3", DbType = "Int NOT NULL")]
		public int Color3
		{
			get
			{
				return this._Color3;
			}
			set
			{
				if (this._Color3 != value)
				{
					this._Color3 = value;
				}
			}
		}

		private string _CharacterName;

		private byte _OperationType;

		private string _ItemClass;

		private int _Count;

		private int _Datestamp;

		private int _Timestamp;

		private int _Color1;

		private int _Color2;

		private int _Color3;
	}
}
