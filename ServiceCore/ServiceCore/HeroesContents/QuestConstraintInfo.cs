using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.QuestConstraintInfo")]
	public class QuestConstraintInfo
	{
		[Column(Storage = "_RowID", DbType = "Int NOT NULL")]
		public int RowID
		{
			get
			{
				return this._RowID;
			}
			set
			{
				if (this._RowID != value)
				{
					this._RowID = value;
				}
			}
		}

		[Column(Storage = "_QuestID", DbType = "NVarChar(64) NOT NULL", CanBeNull = false)]
		public string QuestID
		{
			get
			{
				return this._QuestID;
			}
			set
			{
				if (this._QuestID != value)
				{
					this._QuestID = value;
				}
			}
		}

		[Column(Storage = "_RequiredQuestID", DbType = "NVarChar(64) NOT NULL", CanBeNull = false)]
		public string RequiredQuestID
		{
			get
			{
				return this._RequiredQuestID;
			}
			set
			{
				if (this._RequiredQuestID != value)
				{
					this._RequiredQuestID = value;
				}
			}
		}

		[Column(Storage = "_RequiredStatus", DbType = "Int NOT NULL")]
		public int RequiredStatus
		{
			get
			{
				return this._RequiredStatus;
			}
			set
			{
				if (this._RequiredStatus != value)
				{
					this._RequiredStatus = value;
				}
			}
		}

		private int _RowID;

		private string _QuestID;

		private string _RequiredQuestID;

		private int _RequiredStatus;
	}
}
