using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.QuestSettingsInfo")]
	public class QuestSettingsInfo
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

		[Column(Storage = "_ConditionID", DbType = "Int")]
		public int? ConditionID
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

		[Column(Name = "[Key]", Storage = "_Key", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				if (this._Key != value)
				{
					this._Key = value;
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

		private int? _ConditionID;

		private string _Key;

		private string _Value;
	}
}
