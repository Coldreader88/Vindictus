using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.MDRActionFilter")]
	public class MDRActionFilter
	{
		[Column(Storage = "_Feature", DbType = "NVarChar(50)")]
		public string Feature
		{
			get
			{
				return this._Feature;
			}
			set
			{
				if (this._Feature != value)
				{
					this._Feature = value;
				}
			}
		}

		[Column(Storage = "_QuestIDList", DbType = "NVarChar(250)")]
		public string QuestIDList
		{
			get
			{
				return this._QuestIDList;
			}
			set
			{
				if (this._QuestIDList != value)
				{
					this._QuestIDList = value;
				}
			}
		}

		[Column(Storage = "_ActionType", DbType = "NVarChar(64)")]
		public string ActionType
		{
			get
			{
				return this._ActionType;
			}
			set
			{
				if (this._ActionType != value)
				{
					this._ActionType = value;
				}
			}
		}

		[Column(Storage = "_DamageLimit", DbType = "Int NOT NULL")]
		public int DamageLimit
		{
			get
			{
				return this._DamageLimit;
			}
			set
			{
				if (this._DamageLimit != value)
				{
					this._DamageLimit = value;
				}
			}
		}

		private string _Feature;

		private string _QuestIDList;

		private string _ActionType;

		private int _DamageLimit;
	}
}
