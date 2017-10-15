using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SkillCategory")]
	public class SkillCategory
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

		[Column(Storage = "_CategoryID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if (this._CategoryID != value)
				{
					this._CategoryID = value;
				}
			}
		}

		[Column(Storage = "_KnowledgeSkillID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string KnowledgeSkillID
		{
			get
			{
				return this._KnowledgeSkillID;
			}
			set
			{
				if (this._KnowledgeSkillID != value)
				{
					this._KnowledgeSkillID = value;
				}
			}
		}

		[Column(Storage = "_AdvKnowledgeSkillID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string AdvKnowledgeSkillID
		{
			get
			{
				return this._AdvKnowledgeSkillID;
			}
			set
			{
				if (this._AdvKnowledgeSkillID != value)
				{
					this._AdvKnowledgeSkillID = value;
				}
			}
		}

		[Column(Storage = "_Description", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if (this._Description != value)
				{
					this._Description = value;
				}
			}
		}

		private int _RowID;

		private string _CategoryID;

		private string _KnowledgeSkillID;

		private string _AdvKnowledgeSkillID;

		private string _Description;
	}
}
