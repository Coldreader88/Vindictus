using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.QuestSettingsConditionInfo")]
	public class QuestSettingsConditionInfo
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

		[Column(Storage = "_ConditionID", DbType = "Int NOT NULL")]
		public int ConditionID
		{
			get
			{
				return this._ConditionID;
			}
			set
			{
				if (this._ConditionID != value)
				{
					this._ConditionID = value;
				}
			}
		}

		[Column(Storage = "_Condition", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string Condition
		{
			get
			{
				return this._Condition;
			}
			set
			{
				if (this._Condition != value)
				{
					this._Condition = value;
				}
			}
		}

		[Column(Storage = "_Value", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if (this._Value != value)
				{
					this._Value = value;
				}
			}
		}

		private int _RowID;

		private int _ConditionID;

		private string _Condition;

		private string _Value;
	}
}
