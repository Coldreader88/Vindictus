using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.GuildScoreEvaluateInfo")]
	public class GuildScoreEvaluateInfo
	{
		[Column(Storage = "_rowID", DbType = "Int NOT NULL")]
		public int rowID
		{
			get
			{
				return this._rowID;
			}
			set
			{
				if (this._rowID != value)
				{
					this._rowID = value;
				}
			}
		}

		[Column(Storage = "_Event", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Event
		{
			get
			{
				return this._Event;
			}
			set
			{
				if (this._Event != value)
				{
					this._Event = value;
				}
			}
		}

		[Column(Storage = "_StringValue", DbType = "NVarChar(50)")]
		public string StringValue
		{
			get
			{
				return this._StringValue;
			}
			set
			{
				if (this._StringValue != value)
				{
					this._StringValue = value;
				}
			}
		}

		[Column(Storage = "_IntValue", DbType = "Int")]
		public int? IntValue
		{
			get
			{
				return this._IntValue;
			}
			set
			{
				if (this._IntValue != value)
				{
					this._IntValue = value;
				}
			}
		}

		[Column(Storage = "_EventCode", DbType = "Int NOT NULL")]
		public int EventCode
		{
			get
			{
				return this._EventCode;
			}
			set
			{
				if (this._EventCode != value)
				{
					this._EventCode = value;
				}
			}
		}

		[Column(Storage = "_Score", DbType = "Int NOT NULL")]
		public int Score
		{
			get
			{
				return this._Score;
			}
			set
			{
				if (this._Score != value)
				{
					this._Score = value;
				}
			}
		}

		private int _rowID;

		private string _Event;

		private string _StringValue;

		private int? _IntValue;

		private int _EventCode;

		private int _Score;
	}
}
