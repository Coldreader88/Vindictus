using System;
using System.Data.Linq.Mapping;

namespace GuildService
{
	public class GetGuildStorageBriefLogs
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

		[Column(Storage = "_AddCount", DbType = "Int NOT NULL")]
		public int AddCount
		{
			get
			{
				return this._AddCount;
			}
			set
			{
				if (this._AddCount != value)
				{
					this._AddCount = value;
				}
			}
		}

		[Column(Storage = "_PickCount", DbType = "Int NOT NULL")]
		public int PickCount
		{
			get
			{
				return this._PickCount;
			}
			set
			{
				if (this._PickCount != value)
				{
					this._PickCount = value;
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

		private string _CharacterName;

		private byte _OperationType;

		private int _AddCount;

		private int _PickCount;

		private int _Datestamp;

		private int _Timestamp;
	}
}
